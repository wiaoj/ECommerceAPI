using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Repositories.Endpoints;
using ECommerceAPI.Application.Repositories.MenuEndpoints;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services;
public class AuthorizationEndpointService : IAuthorizationEndpointService {
    private readonly IApplicationService _applicationService;
    private readonly IEndpointReadRepository _endpointReadRepository;
    private readonly IEndpointWriteRepository _endpointWriteRepository;
    private readonly IMenuEndpointReadRepository _menuEndpointReadRepository;
    private readonly IMenuEndpointWriteRepository _menuEndpointWriteRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AuthorizationEndpointService(
        IApplicationService applicationService,
        IEndpointReadRepository endpointReadRepository,
        IEndpointWriteRepository endpointWriteRepository,
        IMenuEndpointReadRepository menuEndpointReadRepository,
        IMenuEndpointWriteRepository menuEndpointWriteRepository,
        RoleManager<ApplicationRole> roleManager) {
        _applicationService = applicationService;
        _endpointReadRepository = endpointReadRepository;
        _endpointWriteRepository = endpointWriteRepository;
        _menuEndpointReadRepository = menuEndpointReadRepository;
        _menuEndpointWriteRepository = menuEndpointWriteRepository;
        _roleManager = roleManager;
    }

    public async Task AssignRoleEndpointAsync(String menuName, String[] roles, String code, Type type) {
        MenuEndpoint? menuEndpoint = await _menuEndpointReadRepository.GetSingleAsync(x => x.Name.Equals(menuName));

        if(menuEndpoint is null) {
            menuEndpoint = new() {
                Id = Guid.NewGuid(),
                Name = menuName,
            };

            await _menuEndpointWriteRepository.AddAsync(menuEndpoint);
            await _menuEndpointWriteRepository.SaveAsync();
        }

        Endpoint? endpoint = await _endpointReadRepository.Table.Include(x => x.MenuEndpoint).Include(x => x.ApplicationRoles).FirstOrDefaultAsync(e => e.Code.Equals(code) && e.MenuEndpoint.Name.Equals(menuName)); //exists gibi bir eporasyon yapılabilir

        if(endpoint is null) {
            var action = _applicationService.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(e => e.Name.Equals(menuName))?.Actions.FirstOrDefault(x => x.Code.Equals(code));

            endpoint = new() {
                Id = Guid.NewGuid(),
                Code = action.Code,
                ActionType = action.ActionType,
                HttpType = action.HttpType,
                Definition = action.Definition,
                MenuEndpoint = menuEndpoint,
            };

            await _endpointWriteRepository.AddAsync(endpoint);

            await _endpointWriteRepository.SaveAsync();
        }

        endpoint.ApplicationRoles.Clear();

        var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

        foreach(var role in appRoles) {
            endpoint.ApplicationRoles.Add(role);
        }


        await _endpointWriteRepository.SaveAsync();
    }

    public async Task<List<String>> GetRolesToEndpointAsync(String code, String menuName) {
        Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.ApplicationRoles).Include(x => x.MenuEndpoint).FirstOrDefaultAsync(x => x.Code.Equals(code) && x.MenuEndpoint.Name.Equals(menuName));

        return endpoint?.ApplicationRoles.Select(r => r.Name).ToList();
    }
}