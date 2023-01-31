using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse> {
    public String UserId { get; set; }
    public String ResetToken { get; set; }
    public String Password { get; set; }
    public String PasswordConfirm { get; set; }
}
