using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Services;
public interface IFileService {
    Task UploadAsync(String path, IFormFileCollection files);
}