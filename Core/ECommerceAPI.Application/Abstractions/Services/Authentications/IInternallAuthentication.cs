namespace ECommerceAPI.Application.Abstractions.Services.Authentications;

public interface IInternallAuthentication {
    Task<DTOs.Token> LoginAsync(String userNameOrEmail, String password);
    Task<DTOs.Token> RefreshTokenLoginAsync(String refreshToken);
}
