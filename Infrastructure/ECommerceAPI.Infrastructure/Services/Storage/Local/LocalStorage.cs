using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.IO;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local;
public class LocalStorage : Storage, ILocalStorage {
    private readonly IWebHostEnvironment _webHostEnvironment;
    public LocalStorage(IWebHostEnvironment webHostEnvironment) {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task DeleteAsync(String pathOrContainerName, String fileName) {
        File.Delete($"{pathOrContainerName}\\{fileName}");
    }

    public List<String> GetFiles(String pathOrContainerName) {
        DirectoryInfo directory = new(pathOrContainerName);
        return directory.GetFiles().Select(file => file.Name).ToList();
    }

    public Boolean HasFile(String pathOrContainerName, String fileName) {
        return File.Exists($"{pathOrContainerName}\\{fileName}");
    }

    public async Task<List<(String fileName, String pathOrContainer)>> UploadAsync(String pathOrContainerName, IFormFileCollection files) {
        String uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);
        if(Directory.Exists(uploadPath) is false) {
            Directory.CreateDirectory(uploadPath);
        }

        List<(String fileName, String path)> datas = new();
        foreach(IFormFile file in files) {

            String newFileName = await FileRenameAsync(pathOrContainerName, file.FileName, HasFile);

            Boolean result = await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
            datas.Add((newFileName, $@"{pathOrContainerName}\{newFileName}"));
        }

        return datas;
        //TODO Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken hata alındığına dair exception oluşturulup fırlatılması gerekiyor
    }

    public async Task<Boolean> CopyFileAsync(String path, IFormFile file) {
        try {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, Convert.ToInt32(Math.Pow(2, 23)), useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        } catch(Exception exception) {
            //TODO log
            throw exception;
        }
    }
}