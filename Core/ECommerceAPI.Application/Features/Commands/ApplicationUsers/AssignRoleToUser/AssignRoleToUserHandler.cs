using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.AssignRoleToUser;
public class AssignRoleToUserHandler : IRequestHandler<AssignRoleToUserRequest, AssignRoleToUserResponse> {
    private readonly IUserService _userService;

    public AssignRoleToUserHandler(IUserService userService) {
        _userService = userService;
    }

    public async Task<AssignRoleToUserResponse> Handle(AssignRoleToUserRequest request, CancellationToken cancellationToken) {
        await _userService.AssignRoleToUserAsync(request.Id, request.Roles);

        return new();
    }
}