using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IUserService {
    Task<CreateUserResponse> CreateAsync(CreateUser model);
    Task UpdateRefreshTokenAsync(ApplicationUser user, String refreshToken, DateTime accessTokenDate, Int32 addOnAccessTokenDate);
    Task UpdatePasswordAsync(String userId, String resetToken, String newPassword);
}