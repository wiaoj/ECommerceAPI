using ECommerceAPI.Infrastructure.Operations;

namespace ECommerceAPI.Infrastructure.Services.Storage;
public class Storage {
    public delegate Boolean HasFile(String pathOrContainer, String fileName);

    //sadece kalıtım alan classlar bu metoda erişebilir
    protected async Task<String> FileRenameAsync(String pathOrContainerName, String fileName, HasFile hasFileMethod) {
        return await Task.Run(async () => {
            String extension = Path.GetExtension(fileName);
            String oldName = Path.GetFileNameWithoutExtension(fileName);
            String newFileName = NameOperation.CharacterRegulatory(oldName);

            if(true)
                newFileName = $"{Guid.NewGuid()}";

            if(hasFileMethod(pathOrContainerName, newFileName)) {
                return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod);
            }

            return $"{newFileName}{extension}";
        });
    }
}