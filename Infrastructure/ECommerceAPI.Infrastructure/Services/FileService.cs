using ECommerceAPI.Application.Services;
using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ECommerceAPI.Infrastructure.Services;
public class FileService : IFileService {
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment) {
        _webHostEnvironment = webHostEnvironment;
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

    private async Task<String> FileRenameAsync(String path, String fileName) {
        return await Task.Run(async () => {
            String extension = Path.GetExtension(fileName);
            String oldName = Path.GetFileNameWithoutExtension(fileName);
            String newFileName = NameOperation.CharacterRegulatory(oldName);

            return $"{newFileName}{extension}";
        });
    }

    public async Task<List<(String fileName, String path)>> UploadAsync(String path, IFormFileCollection files) {
        String uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if(Directory.Exists(uploadPath) is false) {
            Directory.CreateDirectory(uploadPath);
        }

        List<(String fileName, String path)> datas = new();
        List<Boolean> results = new();
        foreach(IFormFile file in files) {
            String fileNewName = await FileRenameAsync(uploadPath, file.FileName);

            Boolean result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            datas.Add((fileNewName, $@"{path}\{fileNewName}"));
            results.Add(result);
        }

        if(results.TrueForAll( results => results.Equals(true))) {
            return datas;
        }
        return null;
        //TODO Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken hata alındığına dair exception oluşturulup fırlatılması gerekiyor
    }
}