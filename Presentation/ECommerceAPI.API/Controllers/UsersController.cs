using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.CreateUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.UpdatePassword;
using MediatR;
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
}