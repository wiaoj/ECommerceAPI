using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Roles.CreateRole;

public class CreateRoleCommandRequest: IRequest<CreateRoleCommandResponse> {
    public String Name { get; set; }
}
