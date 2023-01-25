using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse> {
    private readonly IAuthService _authService;

    public RefreshTokenLoginCommandHandler(IAuthService authService) {
        _authService = authService;
    }

    public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken) {
        Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

        return new() {
            Token = token,
        };
    }
}