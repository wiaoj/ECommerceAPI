using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Repositories.BasketItems;
using ECommerceAPI.Application.Repositories.Baskets;
using ECommerceAPI.Application.Repositories.ComplatedOrders;
using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Application.Repositories.FileRepositories.Files;
using ECommerceAPI.Application.Repositories.FileRepositories.InvoiceFiles;
using ECommerceAPI.Application.Repositories.FileRepositories.ProductImageFiles;
using ECommerceAPI.Application.Repositories.Orders;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.BasketItems;
using ECommerceAPI.Persistence.Repositories.Baskets;
using ECommerceAPI.Persistence.Repositories.ComplatedOrders;
using ECommerceAPI.Persistence.Repositories.Customers;
using ECommerceAPI.Persistence.Repositories.FileRepositories.Files;
using ECommerceAPI.Persistence.Repositories.FileRepositories.InvoiceFiles;
using ECommerceAPI.Persistence.Repositories.FileRepositories.ProductImageFiles;
using ECommerceAPI.Persistence.Repositories.Orders;
using ECommerceAPI.Persistence.Repositories.Products;
using ECommerceAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;
public static class ServiceRegistration {
    public static void AddPersistenceServices(this IServiceCollection services) {
        services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configuration.GetPostgreSQLConnectionString));
        // Adding user & user role
        services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<ECommerceAPIDbContext>()
        .AddDefaultTokenProviders(); //identity üzerinden generate password metodunu kullanmak için ekliyoruz

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

        services.AddScoped<IBasketReadRepository, BasketReadRepository>();
        services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

        services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
        services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

        services.AddScoped<IComplatedOrderReadRepository, ComplatedOrderReadRepository>();
        services.AddScoped<IComplatedOrderWriteRepository, ComplatedOrderWriteRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IInternallAuthentication, AuthService>();
        services.AddScoped<IExternalAuthentication, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}