using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Roles.UpdateRole;

public class UpdateRoleCommandRequest: IRequest<UpdateRoleCommandResponse> {
    public String Id { get; set; }
    public String Name { get; set; }
}
