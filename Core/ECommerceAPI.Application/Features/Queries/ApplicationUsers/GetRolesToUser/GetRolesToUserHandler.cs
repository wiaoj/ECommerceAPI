using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.ApplicationUsers.GetRolesToUser;
public class GetRolesToUserHandler : IRequestHandler<GetRolesToUserRequest, GetRolesToUserResponse> {
    private readonly IUserService _userService;

    public GetRolesToUserHandler(IUserService userService) {
        _userService = userService;
    }

    public async Task<GetRolesToUserResponse> Handle(GetRolesToUserRequest request, CancellationToken cancellationToken) {
        var userRoles = await _userService.GetRolesToUserAsync(request.Id);
        return new() {
            Roles = userRoles,
        };
    }
}