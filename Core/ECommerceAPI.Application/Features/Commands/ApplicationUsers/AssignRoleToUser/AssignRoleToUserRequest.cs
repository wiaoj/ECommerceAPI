using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.AssignRoleToUser;

public class AssignRoleToUserRequest : IRequest<AssignRoleToUserResponse> {
    public String Id { get; set; }
    public String[] Roles { get; set; }
}
