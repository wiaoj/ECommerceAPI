using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Application.Repositories.Orders;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Customers;
using ECommerceAPI.Persistence.Repositories.Orders;
using ECommerceAPI.Persistence.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;
public static class ServiceRegistration {
	public static void AddPersistenceServices(this IServiceCollection services) {
		services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configuration.GetPostgreSQLConnectionString));

		services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
		services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

		services.AddScoped<IOrderReadRepository, OrderReadRepository>();
		services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

		services.AddScoped<IProductReadRepository, ProductReadRepository>();
		services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
	}
}