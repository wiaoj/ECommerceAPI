using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse {
    public Token Token { get; set; }
}