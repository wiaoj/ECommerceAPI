using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;

public class FacebookLoginCommandRequest: IRequest<FacebookLoginCommandResponse> {
    public String AuthToken { get; set; } // Diğer field'lara ihtiyacımız yok
}