using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.PasswordReset;
public class PasswordResetCommandRequest: IRequest<PasswordResetCommandResponse> {
    public String Email { get; set; }
}