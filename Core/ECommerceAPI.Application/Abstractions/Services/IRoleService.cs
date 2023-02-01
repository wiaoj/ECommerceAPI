namespace ECommerceAPI.Application.Abstractions.Services;
public interface IRoleService {
    Task<(IDictionary<String, String>, Int32)> GetAllRoles(Int32 page, Int32 size);
    Task<(String id, String name)> GetRoleById(String id);
    Task<Boolean> CreateRoleAsync(String name);
    Task<Boolean> DeleteRoleAsync(String id);
    Task<Boolean> UpdateRoleAsync(String id, String name);
}