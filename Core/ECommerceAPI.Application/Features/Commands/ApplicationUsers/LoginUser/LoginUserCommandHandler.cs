using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse> {
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService) {
        _authService = authService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken) {
        Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);

        return new LoginUserSuccessCommandResponse() {
            Token = token
        };
    }
}