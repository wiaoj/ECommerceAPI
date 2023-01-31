using ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.PasswordReset;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.RefreshTokenLogin;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase {
    private readonly ISender _sender;

    public AuthsController(ISender mediator) {
        _sender = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest) {
        LoginUserCommandResponse result = await _sender.Send(loginUserCommandRequest);
        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest) {
        RefreshTokenLoginCommandResponse result = await _sender.Send(refreshTokenLoginCommandRequest);
        return Ok(result);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest) {
        GoogleLoginCommandResponse response = await _sender.Send(googleLoginCommandRequest);
        return Ok(response);
    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest) {
        FacebookLoginCommandResponse response = await _sender.Send(facebookLoginCommandRequest);
        return Ok(response);
    }

    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest) {
        PasswordResetCommandResponse response = await _sender.Send(passwordResetCommandRequest);
        return Ok(response);
    }

    [HttpPost("verify-reset-token")]
    public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest) {
        VerifyResetTokenCommandResponse response = await _sender.Send(verifyResetTokenCommandRequest);
        return Ok(response);
    }
}