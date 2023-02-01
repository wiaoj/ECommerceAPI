using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Roles.DeleteRole;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse> {
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService) {
        _roleService = roleService;
    }

    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken) {
        Boolean result = await _roleService.DeleteRoleAsync(request.Id);
        return new() {
            Succeeded = result,
        };
    }
}