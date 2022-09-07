using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Abstractions.Storage.Local;
public interface ILocalStorage : IStorage {
    Task<Boolean> CopyFileAsync(String path, IFormFile file);
}