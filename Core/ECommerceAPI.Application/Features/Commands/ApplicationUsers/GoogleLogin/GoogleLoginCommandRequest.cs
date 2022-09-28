using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;

public class GoogleLoginCommandRequest: IRequest<GoogleLoginCommandResponse> {
    public String Id { get; set; }
    public String IdToken { get; set; }
    public String Name { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String PhotoUrl { get; set; }
    public String Provider { get; set; }
}
