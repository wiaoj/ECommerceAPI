using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Abstractions.Storage;
public interface IStorage {
    Task<List<(String fileName, String pathOrContainer)>> UploadAsync(String pathOrContainerName, IFormFileCollection files);
    Task DeleteAsync(String pathOrContainerName, String fileName);
    List<String> GetFiles(String pathOrContainerName);
    Boolean HasFile(String pathOrContainerName, String fileName);
}