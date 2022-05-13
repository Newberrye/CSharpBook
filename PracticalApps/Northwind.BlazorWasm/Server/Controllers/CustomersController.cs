using Microsoft.AspNetCore.Mvc; // [ApiController], [Route]
using Microsoft.EntityFrameworkCore; // ToListAsync, FirstOrDefaultAsync
using Packt.Shared; // NorthwindContext, Customer

namespace Northwind.BlazorWasm.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class  CustomersController : ControllerBase
{
    private readonly NorthwindContext db;

    public CustomersController(NorthwindContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await db.Customers.ToListAsync();
    }

    [HttpGet("in/{country}")]
    public async Task<List<Customer>> GetCustomersAsync(string country)
    {
        return await db.Customers
            .Where(c => c.Country == country).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Customer?> GetCustomerAsync(string id)
    {
        return await db.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    [HttpPost]
    public async Task<Customer?> CreateCustomerAsync(Customer customer)
    {
        Customer? existing = await db.Customers.FirstOrDefaultAsync
            (c => c.CustomerId == customer.CustomerId);

        if (existing == null)
        {
            db.Customers.Add(customer);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return customer;
            }
        }
        return existing;
    }

    [HttpPut]
    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        db.Entry(customer).State = EntityState.Modified;
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            return customer;
        }
        return null;
    }

    [HttpDelete("{id}")]
    public async Task<int> DeleteCustomerAsync(string id)
    {
        Customer? customer = await db.Customers.FirstOrDefaultAsync
            (c => c.CustomerId == id);

        if (customer != null)
        {
            db.Customers.Remove(customer);
            int affected = await db.SaveChangesAsync();
            return affected;
        }
        return 0;
    }
}
