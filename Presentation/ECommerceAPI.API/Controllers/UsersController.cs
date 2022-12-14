using ECommerceAPI.Application.Features.Commands.ApplicationUsers.CreateUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest) {
        CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest) {
        LoginUserCommandResponse result = await _mediator.Send(loginUserCommandRequest);
        return Ok(result);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest) {
        GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
        return Ok(response);
    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest) {
        FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
        return Ok(response);
    }
}