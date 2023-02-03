using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services;
public class RoleService : IRoleService {
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager) {
        _roleManager = roleManager;
    }

    public async Task<Boolean> CreateRoleAsync(String name) {
        IdentityResult result = await _roleManager.CreateAsync(new() {
            Id = Guid.NewGuid().ToString(),
            Name = name
        });
        return result.Succeeded;
    }

    public async Task<Boolean> DeleteRoleAsync(String id) {
        IdentityResult result = await _roleManager.DeleteAsync(new() { Id = id });
        return result.Succeeded;
    }

    public async Task<(IDictionary<String, String>, Int32)> GetAllRoles(Int32 page, Int32 size) {
        IQueryable<ApplicationRole>? query = _roleManager.Roles;

        query = page is -1 && size is -1
                    ? query
                    : query.Skip(page * size).Take(size);

        return (await query.ToDictionaryAsync(role => role.Id, role => role.Name), await query.CountAsync());
    }

    public async Task<(String id, String name)> GetRoleById(String id) {
        String role = await _roleManager.GetRoleIdAsync(new() {
            Id = id
        });

        return (id, role);
    }

    public async Task<Boolean> UpdateRoleAsync(String id, String name) {
        IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
        return result.Succeeded;
    }
}