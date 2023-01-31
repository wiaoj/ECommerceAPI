using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.VerifyResetToken;
public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse> {
    private readonly IAuthService _authService;

    public VerifyResetTokenCommandHandler(IAuthService authService) {
        _authService = authService;
    }

    public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken) {
        (Boolean state, String email) data = await _authService.VerifyResetTokenAsync(request.UserId, request.ResetToken);

        return new() {
            State = data.state,
            Email = data.email,
        };
    }
}