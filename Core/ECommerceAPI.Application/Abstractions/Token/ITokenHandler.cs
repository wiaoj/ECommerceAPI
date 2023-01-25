using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Token;
public interface ITokenHandler {
    public DTOs.Token CreateAccessToken(ApplicationUser applicationUser);
    public DTOs.Token CreateAccessToken(ApplicationUser applicationUser, Int32 seconds);
    public String CreateRefreshToken();
}