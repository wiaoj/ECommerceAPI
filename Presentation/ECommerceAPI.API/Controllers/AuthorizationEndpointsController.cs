using ECommerceAPI.Application.Features.Commands.AuthorizationEndpoints.AssignRoleEndpoint;
using ECommerceAPI.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorizationEndpointsController : ControllerBase {
    private readonly ISender _sender;

    public AuthorizationEndpointsController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("get-roles-to-endpoint")]
    public async Task<IActionResult> GetRolesToEndpoint([FromBody]  GetRolesToEndpointQueryRequest request) {
        GetRolesToEndpointQueryResponse response = await _sender.Send(request);
        return Ok(response.Roles);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest request) {
        request.Type = typeof(Program);
        AssignRoleEndpointCommandResponse response = await _sender.Send(request);
        return Ok(response);
    }
}