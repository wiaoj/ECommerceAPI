using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.CreateUser;
public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse> {
    public String NameSurname { get; set; }
    public String UserName { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    public String PasswordConfirm { get; set; }
}