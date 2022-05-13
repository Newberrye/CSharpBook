using Microsoft.AspNetCore.Mvc.Formatters;
using Packt.Shared; // AddNorthWindContext

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI; // Submit Method

using Microsoft.AspNetCore.HttpLogging; //HttpLoggingFields

using Northwind.WebApi.Repositories;

using static System.Console;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5002/");

// Add services to the container.
builder.Services.AddNorthwindContext();

builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters:");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter == null)
        {
            WriteLine($"    {formatter.GetType().Name}");
        }
        else // OutputFormatter class has Supported Media Types
        {
            WriteLine($"  {mediaFormatter.GetType().Name}, Media types: {string.Join(", ", mediaFormatter.SupportedMediaTypes)}");
        }
    }
})
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(customer =>
{
    customer.SwaggerDoc("v1", new()
    {
        Title = "Northwind Service API",
        Version = "v1"
    });
});
builder.Services.AddScoped<ICustomerRepository, CustomerRespository>();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.ResponseBodyLogLimit = 4096;
    options.RequestBodyLogLimit = 4096;
});

builder.Services.AddCors();

builder.Services.AddHealthChecks()
 .AddDbContextCheck<NorthwindContext>();



var app = builder.Build();

app.UseHttpLogging();

app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins(
    "https://localhost:5001" // allow requests from the MVC client
    );
});

app.UseMiddleware<SecurityHeaders>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(customer =>
    {
        customer.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");

        customer.SupportedSubmitMethods(new[]
        {
            SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks(path: "/howdoyoufeel");

app.MapControllers();

app.Run();
