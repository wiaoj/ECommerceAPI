using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Roles.GetAllRoles;

public class GetRolesQueryRequest : IRequest<GetRolesQueryResponse> {
    public Int32 Page { get; set; } = 0;
    public Int32 Size { get; set; } = 5;
}
