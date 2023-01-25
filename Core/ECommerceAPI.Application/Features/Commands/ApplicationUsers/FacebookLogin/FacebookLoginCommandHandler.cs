using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;
public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse> {
    private readonly IAuthService _authService;

    public FacebookLoginCommandHandler(IAuthService authService) {
        _authService = authService;
    }

    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken) {

        Token token = await _authService.FacebookLoginAsync(request.AuthToken);

        return new() {
            Token = token,
        };
    }
}