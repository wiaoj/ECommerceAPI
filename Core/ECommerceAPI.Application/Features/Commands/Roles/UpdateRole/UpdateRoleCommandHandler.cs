using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Roles.UpdateRole;
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse> {
    private readonly IRoleService _roleService;

    public UpdateRoleCommandHandler(IRoleService roleService) {
        _roleService = roleService;
    }

    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken) {
        Boolean result = await _roleService.UpdateRoleAsync(request.Id, request.Name);

        return new() {
            Succeeded = result,
        };
    }
}