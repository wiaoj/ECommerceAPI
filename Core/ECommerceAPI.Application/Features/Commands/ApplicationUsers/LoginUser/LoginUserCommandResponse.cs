using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;

public class LoginUserCommandResponse { }
public class LoginUserSuccessCommandResponse : LoginUserCommandResponse {
    public Token Token { get; set; }
}
public class LoginUserErrorCommandResponse : LoginUserCommandResponse {
    public String Message { get; set; }
}