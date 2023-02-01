using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Roles.GetRoleById;

public class GetRoleByIdQueryRequest: IRequest<GetRoleByIdQueryResponse> {
    public String Id { get; set; }
}