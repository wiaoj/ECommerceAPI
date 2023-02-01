using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Roles.GetAllRoles;
public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse> {
    private readonly IRoleService _roleService;

    public GetRolesQueryHandler(IRoleService roleService) {
        _roleService = roleService;
    }

    public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken) {
        (IDictionary<String, String> roles, Int32 count) roles = await _roleService.GetAllRoles(request.Page, request.Size);
        return new() {
            Roles = roles.roles,
            TotalRoleCount = roles.count,
        };
    }
}