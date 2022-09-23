using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;
public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>{
    public String UsernameOrEmail { get; set; }
    public String Password { get; set; }
}