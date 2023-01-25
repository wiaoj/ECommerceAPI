using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;
public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse> {
    private readonly IExternalAuthentication _externalAuthService;

    public GoogleLoginCommandHandler(IExternalAuthentication externalAuthService) {
        _externalAuthService = externalAuthService;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken) {

        Token token = await _externalAuthService.GoogleLoginAsync(request.IdToken);

        return new() {
            Token = token
        };
    }
}