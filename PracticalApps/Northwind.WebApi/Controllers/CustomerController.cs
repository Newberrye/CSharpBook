using Microsoft.AspNetCore.Mvc; // [Route], [ApiController], ControllerBase

using Packt.Shared; // Customer
using Northwind.WebApi.Repositories; // ICustomerRepository

namespace Northwind.WebApi.Controllers;

// base address: api/customers
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository repository;

    public CustomersController(ICustomerRepository repo)
    {
        this.repository = repo;
    }

    // GET: api/customers
    // GET: api/customers/?country=[country]
    // this will always return a list of customers but might be empty
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return await repository.RetrieveAllAsync();
        }
        else
        {
            return (await repository.RetrieveAllAsync())
                .Where(customer => customer.Country == country);
        }
    }

    // GET: api/customers/[id]
    [HttpGet("{id}", Name = nameof(GetCustomer))] // named route
    [ProducesResponseType(200, Type= typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        Customer? customer = await repository.RetrieveAsync(id);
        if (customer == null)
        {
            return NotFound(); // 404 Not Found response
        }
        return Ok(customer); // 200 OK response
    }

    // POST: api/customers
    // BODY: Customer (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        if (customer == null)
        {
            return BadRequest(); // 400
        }

        Customer? addedCustomer = await repository.CreateAsync(customer);

        if (addedCustomer == null)
        {
            return BadRequest("Repository failed to create customer");
        }
        else
        {
            return CreatedAtRoute(
                routeName: nameof(GetCustomer),
                routeValues: new { id = addedCustomer.CustomerId.ToLower() },
                value: addedCustomer);
        }
    }


    // PUT: api/customers/[id]
    // BODY: Customer (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] Customer customer)
    {
        id = id.ToUpper();
        customer.CustomerId = customer.CustomerId.ToUpper();

        if (customer == null || customer.CustomerId != id)
        {
            return BadRequest();
        }

        Customer? existing = await repository.RetrieveAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await repository.UpdateAsync(id, customer);

        return new NoContentResult();
    }

    // DELETE: api/customers/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        // example of taking control of a BadRequest
        if (id == "bad")
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5001/customers/failed-to-delete",
                Title = $"Customer ID {id} found but failed to delete.",
                Detail = "More details like Company Name, Country and so on.",
                Instance = HttpContext.Request.Path
            };
            return BadRequest(problemDetails); // 400 Bad Request
        }


        Customer? existing = await repository.RetrieveAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        bool? deleted = await repository.DeleteAsync(id);

        if (deleted.HasValue && deleted.Value)
        {
            return new NoContentResult();
        }
        else
        {
            return BadRequest($"Customer {id} was found but failed to delete.");
        }
    }


}
