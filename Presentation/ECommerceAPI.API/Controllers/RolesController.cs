using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Roles.CreateRole;
using ECommerceAPI.Application.Features.Commands.Roles.DeleteRole;
using ECommerceAPI.Application.Features.Commands.Roles.UpdateRole;
using ECommerceAPI.Application.Features.Queries.Roles.GetAllRoles;
using ECommerceAPI.Application.Features.Queries.Roles.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase {
    private readonly ISender _sender;

    public RolesController(ISender sender) {
        _sender = sender;
    }

    [HttpGet]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Roles,
        ActionType = ActionType.Reading,
        Definition = "Get Roles"
        )]
    public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest) {
        GetRolesQueryResponse response = await _sender.Send(getRolesQueryRequest);
        return Ok(response);
    }

    [HttpGet("{Id}")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Roles,
        ActionType = ActionType.Reading,
        Definition = "Get Role By Id"
        )]
    public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest) {
        GetRoleByIdQueryResponse response = await _sender.Send(getRoleByIdQueryRequest);
        return Ok(response);
    }

    [HttpPost]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Roles,
        ActionType = ActionType.Writing,
        Definition = "Create Role"
        )]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest request) {
        CreateRoleCommandResponse response = await _sender.Send(request);
        return Ok(response);
    }

    [HttpPut("{Id}")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Roles,
        ActionType = ActionType.Updating,
        Definition = "Update Role"
        )]
    public async Task<IActionResult> UpdateRole([FromRoute, FromBody] UpdateRoleCommandRequest request) {
        UpdateRoleCommandResponse response = await _sender.Send(request);
        return Ok(response);
    }

    [HttpDelete("{Id}")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Roles,
        ActionType = ActionType.Deleting,
        Definition = "Delete Role"
        )]
    public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest request) {
        DeleteRoleCommandResponse response = await _sender.Send(request);
        return Ok(response);
    }
}