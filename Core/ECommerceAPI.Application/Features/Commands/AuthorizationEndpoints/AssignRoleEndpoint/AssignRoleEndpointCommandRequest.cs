using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleEndpoint;

public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse> {
    public String MenuName { get; set; }
    public String[] Roles { get; set; }
    public String Code { get; set; }
    public Type? Type { get; set; }
}