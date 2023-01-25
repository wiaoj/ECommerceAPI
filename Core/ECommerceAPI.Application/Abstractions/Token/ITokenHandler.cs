namespace ECommerceAPI.Application.Abstractions.Token;
public interface ITokenHandler {
    public DTOs.Token CreateAccessToken();
    public DTOs.Token CreateAccessToken(Int32 seconds);
    public String CreateRefreshToken();
}