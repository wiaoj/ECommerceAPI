using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint;

public class GetRolesToEndpointQueryRequest : IRequest<GetRolesToEndpointQueryResponse> {
    public String Code { get; set; }
    public String MenuName { get; set; }
}
