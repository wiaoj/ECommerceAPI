using ECommerceAPI.Infrastructure.Operations;

namespace ECommerceAPI.Infrastructure.Services;
public class FileService {


    private async Task<String> FileRenameAsync(String path, String fileName) {
        return await Task.Run(async () => {
            String extension = Path.GetExtension(fileName);
            String oldName = Path.GetFileNameWithoutExtension(fileName);
            String newFileName = NameOperation.CharacterRegulatory(oldName);

            return $"{newFileName}{extension}";
        });
    }
}