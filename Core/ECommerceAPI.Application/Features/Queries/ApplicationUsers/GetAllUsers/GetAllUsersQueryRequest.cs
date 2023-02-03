using MediatR;

namespace ECommerceAPI.Application.Features.Queries.ApplicationUsers.GetAllUsers;

public class  GetAllUsersQueryRequest: IRequest<GetAllUsersQueryResponse> {
    public Int32 Page { get; set; }
    public Int32 Size { get; set; }
}
