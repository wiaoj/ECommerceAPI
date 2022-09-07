using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Enums;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure;
public static class ServiceRegistration {
    public static void AddInfrastructureServices(this IServiceCollection services) {
        services.AddScoped<IStorageService, StorageService>();
        services.AddStorage<LocalStorage>();
    }

    public static void AddStorage<StorageType>(this IServiceCollection services) where StorageType : class, IStorage {
        services.AddScoped<IStorage, StorageType>();
    }

    public static void AddStorage(this IServiceCollection services, StorageType storageType) {
        switch(storageType) {
            case StorageType.Local:
                services.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
               // services.AddScoped<IStorage, LocalStorage>();

                break;
            case StorageType.AWS:
                //services.AddScoped<IStorage, LocalStorage>();

                break;
            default:
                services.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}