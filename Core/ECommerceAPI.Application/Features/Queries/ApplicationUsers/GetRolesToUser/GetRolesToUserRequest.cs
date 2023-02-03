using MediatR;

namespace ECommerceAPI.Application.Features.Queries.ApplicationUsers.GetRolesToUser;

public class GetRolesToUserRequest : IRequest<GetRolesToUserResponse> {
    public String Id { get; set; }
}
