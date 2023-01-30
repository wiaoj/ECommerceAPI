using ECommerceAPI.Application.Abstractions.Services;
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

    [HttpGet]
    public async Task<IActionResult> ExampleMailTest() {
        await _mailService.SendMessageAsync("bertandeniz7@gmail.com", "Mail örneği", "<h1>Mail Örneği</h1>");
        return Ok();
    }


}