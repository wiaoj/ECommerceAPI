using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Services;
public interface IFileService {
    Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files);
    //Task<String> FileRenameAsync(String fileName);
    Task<Boolean> CopyFileAsync(String path, IFormFile file);
}