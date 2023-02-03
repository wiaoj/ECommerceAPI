using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleEndpoint;

public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse> {
    private readonly IAuthorizationEndpointService _authorizationEndpointService;

    public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService) {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken) {
        await _authorizationEndpointService.AssignRoleEndpointAsync(request.MenuName, request.Roles, request.Code, request.Type);

        return new() { };
    }
}