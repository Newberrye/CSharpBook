using System.Net.Http.Json; // GetFromJsonAsync, ReadFromJsonAsync
using Packt.Shared; // Customer

namespace Northwind.BlazorWasm.Client.Data;

public class NorthwindService : INorthwindService
{
    private readonly HttpClient http;

    public NorthwindService(HttpClient http)
    {
        this.http = http;
    }

    public Task<List<Customer>> GetCustomersAsync()
    {
        return http.GetFromJsonAsync<List<Customer>>("api/customers");
    }

    public Task<List<Customer>> GetCustomersAsync(string country)
    {
        return http.GetFromJsonAsync<List<Customer>>($"api/customers/in/{country}");
    }

    public Task<Customer> GetCustomerAsync(string id)
    {
        return http.GetFromJsonAsync<Customer>($"api/customers/in/{id}");
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        HttpResponseMessage response = await http.PostAsJsonAsync("api/customers", customer);

        return await response.Content
            .ReadFromJsonAsync<Customer>();
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        HttpResponseMessage response = await http.PutAsJsonAsync("api/customers", customer);

        return await response.Content
            .ReadFromJsonAsync<Customer>();
    }

    public async Task DeleteCustomerAsync(string id)
    {
        HttpResponseMessage response = await http.DeleteAsync($"api/customers/{id}");
    }
}