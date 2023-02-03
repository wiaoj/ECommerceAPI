namespace ECommerceAPI.Application.Abstractions.Services;
public interface IAuthorizationEndpointService {
    public Task AssignRoleEndpointAsync(String menuName,String[] roles, String code, Type type);
    public Task<List<String>> GetRolesToEndpointAsync(String code, String menuName);
}