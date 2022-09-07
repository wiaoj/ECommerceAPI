using ECommerceAPI.Application.Services;
using ECommerceAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure;
public static class ServiceRegistration {
    public static void AddInfrastructureServices(this IServiceCollection services) {
        services.AddScoped<IFileService, FileService>();
    }
}