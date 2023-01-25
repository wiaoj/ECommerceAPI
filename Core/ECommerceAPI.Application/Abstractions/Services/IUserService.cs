using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IUserService {
    Task<CreateUserResponse> CreateAsync(CreateUser model);
    Task UpdateRefreshToken(ApplicationUser user, String refreshToken, DateTime accessTokenDate, Int32 addOnAccessTokenDate);
}