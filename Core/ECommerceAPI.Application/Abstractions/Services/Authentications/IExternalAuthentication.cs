namespace ECommerceAPI.Application.Abstractions.Services.Authentications;
public interface IExternalAuthentication {
    Task<DTOs.Token> FacebookLoginAsync(String authToken);
    Task<DTOs.Token> GoogleLoginAsync(String idToken);
}