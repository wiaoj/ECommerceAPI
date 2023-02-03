using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint;
public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse> {
    private readonly IAuthorizationEndpointService _authorizationEndpointService;

    public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService) {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken) {
        var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code, request.MenuName);

        return new() {
            Roles = datas,
        };
    }
}
