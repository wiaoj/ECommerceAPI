using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Roles.DeleteRole;

public class DeleteRoleCommandRequest: IRequest<DeleteRoleCommandResponse> {
    public String Id { get; set; }
}