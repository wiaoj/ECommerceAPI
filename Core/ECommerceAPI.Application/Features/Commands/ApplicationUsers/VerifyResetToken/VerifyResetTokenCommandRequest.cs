using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.VerifyResetToken;

public class VerifyResetTokenCommandRequest:IRequest<VerifyResetTokenCommandResponse> {
    public String UserId { get; set; }
    public String ResetToken { get; set; }
}