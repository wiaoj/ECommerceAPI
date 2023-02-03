using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.AssignRoleToUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.CreateUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.UpdatePassword;
using ECommerceAPI.Application.Features.Queries.ApplicationUsers.GetAllUsers;
using ECommerceAPI.Application.Features.Queries.ApplicationUsers.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IMailService _mailService;

    public UsersController(IMediator mediator, IMailService mailService) {
        _mediator = mediator;
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest) {
        CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest) {
        UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Users,
        ActionType = ActionType.Reading,
        Definition = "Get All Users"
        )]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request) {
        GetAllUsersQueryResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet, Route("get-roles-to-user/{Id}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Users,
        ActionType = ActionType.Reading,
        Definition = "Get Roles To User"
        )]
    public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserRequest request) {
        GetRolesToUserResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost, Route("assign-role-to-user")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Users,
        ActionType = ActionType.Writing,
        Definition = "Assign Role To User"
        )]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserRequest request) {
        AssignRoleToUserResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}