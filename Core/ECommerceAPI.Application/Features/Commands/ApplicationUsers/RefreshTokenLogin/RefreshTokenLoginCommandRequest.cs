using Google.Apis.Auth.OAuth2.Requests;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.RefreshTokenLogin;
public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse> {
    public String RefreshToken { get; set; }
}