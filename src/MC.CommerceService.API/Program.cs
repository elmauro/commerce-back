using MC.CommerceService.API.Data;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using MC.CommerceService.API.Validators;
using MC.CommerceService.API.Data.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using MC.CommerceService.API.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithExposedHeaders("Location");
        });
});

// Setting up logging with Serilog using settings from the app's configuration
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Configures HTTP logging to record the duration of HTTP requests
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.Duration;
});

builder.Services.AddControllers();

// Adds caching services to store data in memory
builder.Services.AddMemoryCache();

// Configures the database context for ProductDB using mssql
builder.Services.AddDbContext<CommerceDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));

// Registers repository and service classes for dependency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

// Registers AutoMapper to manage object-object mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

// Sets up Fluent Validation for validating models
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrderProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCategoryValidator>();

// Configures Swagger to help create interactive API documentation
ConfigureSwaggerOptions.AddSwagger(builder.Services);

// Adds MediatR for implementing mediator pattern in handling requests
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Middleware to enable HTTP logging
app.UseHttpLogging();

// If the app is not in production, enable Swagger UI
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commerce API V1");
        c.RoutePrefix = "commerce";  // Serve Swagger UI under '/commerce'
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "commerce/index.html"); // Redirect root URL to Swagger UI
    app.UseRewriter(option);
}

// Logs HTTP requests using Serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();

// Indicates the part of the program to exclude from code coverage measurement
[ExcludeFromCodeCoverage]
public partial class Program { }
