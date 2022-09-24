namespace ECommerceAPI.Application.Abstractions.Token;
public interface ITokenHandler {
    public DTOs.Token CreateAccessToken();
}