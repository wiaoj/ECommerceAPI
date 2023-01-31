using ECommerceAPI.Application.DTOs.Configuration;

namespace ECommerceAPI.Application.Abstractions.Services.Configurations;
public interface IApplicationService {
    public List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}