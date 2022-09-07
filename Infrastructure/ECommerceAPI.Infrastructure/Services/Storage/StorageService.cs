using ECommerceAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage;
internal class StorageService : IStorageService {
    readonly IStorage _storage;

    public StorageService(IStorage storage) {
        this._storage = storage;
    }

    public String StorageName { get => _storage.GetType().Name; }

    public async Task DeleteAsync(String pathOrContainerName, String fileName) {
        await _storage.DeleteAsync(pathOrContainerName,fileName);
    }

    public List<String> GetFiles(String pathOrContainerName) {
        return _storage.GetFiles(pathOrContainerName);
    }

    public Boolean HasFile(String pathOrContainerName, String fileName) {
        return HasFile(pathOrContainerName, fileName);
    }

    public async Task<List<(String fileName, String pathOrContainer)>> UploadAsync(String pathOrContainerName, IFormFileCollection files) {
        return await _storage.UploadAsync(pathOrContainerName, files);
    }
}
