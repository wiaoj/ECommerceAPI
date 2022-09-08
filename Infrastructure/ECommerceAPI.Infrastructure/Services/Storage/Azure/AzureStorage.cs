using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerceAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Infrastructure.Services.Storage.Azure;
public class AzureStorage : Storage, IAzureStorage {
    readonly BlobServiceClient _blobServiceClient; //azureye bağlanıyor
    BlobContainerClient _blobContainerClient; //dosya işlemi yapıyor

    public AzureStorage(IConfiguration configuration) {
        _blobServiceClient = new(configuration["Storage:Azure"]);
    }

    public async Task DeleteAsync(String pathOrContainerName, String fileName) {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<String> GetFiles(String pathOrContainerName) {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        return _blobContainerClient.GetBlobs().Select(file => file.Name).ToList();
    }

    public Boolean HasFile(String pathOrContainerName, String fileName) {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        return _blobContainerClient.GetBlobs().Any(file => file.Name.Equals(fileName));
    }

    public async Task<List<(String fileName, String pathOrContainer)>> UploadAsync(String pathOrContainerName, IFormFileCollection files) {
        //container ismi özel karakter olmadan yazılmalıdır resources/images gibi olamaz
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(String fileName, String pathOrContainer)> datas = new();
        foreach(IFormFile file in files) {
            String newFileName = await FileRenameAsync(pathOrContainerName, file.Name, HasFile);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((newFileName, pathOrContainerName));
        }

        return datas;
    }
}