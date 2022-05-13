using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using System.Collections.Concurrent; // ConcurrentDictionary

using Packt.Shared; // Customer

namespace Northwind.WebApi.Repositories;


/// <summary>
/// This class is meant to store customer information on cache, and handle creating/updating/deleting from the database and cache.
/// </summary>
public class CustomerRespository : ICustomerRepository
{
    // using a static thread-safe dictionary field to cache customer
    private static ConcurrentDictionary<string, Customer>? customersCache;

    // use an instance data context field because it should not be cached
    private NorthwindContext db;

    public CustomerRespository(NorthwindContext injectedContext)
    {
        db = injectedContext;

        // pre-load customers from database as norm Dictionarry with
        // Customer ID as key.
        // Converts to thread-safe dictionary
        if (customersCache == null)
        {
            customersCache = new ConcurrentDictionary<string, Customer>(
                db.Customers.ToDictionary(customer => customer.CustomerId));
        }
    }

    /// <summary>
    /// Adds new customers to database and cache.
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public async Task<Customer?> CreateAsync(Customer customer)
    {
        // normalized CustomerID to uppercase
        customer.CustomerId = customer.CustomerId.ToUpper();

        // add to database with EF Core
        EntityEntry<Customer> added = await db.Customers.AddAsync(customer);
        int affected = await db.SaveChangesAsync();

        if (affected == 1)
        {
            if (customersCache is null) return customer;
            // if customer is new, add it to cache else call UpdateCache
            return customersCache.AddOrUpdate(customer.CustomerId, customer, UpdateCache);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Gets all values from cache.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Customer>> RetrieveAllAsync()
    {
        // gets from cache for performance
        return Task.FromResult(customersCache is null
            ? Enumerable.Empty<Customer>() : customersCache.Values);
    }

    /// <summary>
    /// Gets a value from cache.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Customer?> RetrieveAsync(string id)
    {
        // gets from cache for performance
        id = id.ToUpper();
        if (customersCache is null) return null!;
        customersCache.TryGetValue(id, out Customer? customer);
        return Task.FromResult(customer);
    }

    /// <summary>
    /// Updates cache when a change occurs in database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customer"></param>
    /// <returns></returns>
    private Customer UpdateCache(string id, Customer customer)
    {
        Customer? old;
        if (customersCache is not null)
        {
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, customer, old))
                {
                    return customer;
                }
            }
        }
        return null!;
    }

    /// <summary>
    /// Updates database and then the cache for any changes in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customer"></param>
    /// <returns></returns>
    public async Task<Customer> UpdateAsync(string id, Customer customer)
    {
        // normalize
        id = id.ToUpper();
        customer.CustomerId = customer.CustomerId.ToUpper();

        //update database
        db.Customers.Update(customer);
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            // update cache
            return UpdateCache(id, customer);
        }
        return null!;
    }


    /// <summary>
    /// Deletes an item from database then updates cache.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool?> DeleteAsync(string id)
    {
        // normalize
        id = id.ToUpper();

        // remove from database
        Customer? customer = db.Customers.Find(id);
        if (customer is null) return null;
        db.Customers.Remove(customer);
        int affected = await db.SaveChangesAsync();
        if (affected == 1)
        {
            if (customersCache is null) return null;
            //remove from cache
            return customersCache.TryRemove(id, out customer);
        }
        else
        {
            return null;
        }
    }


}
