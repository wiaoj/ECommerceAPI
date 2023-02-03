using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IUserService {
    Task<CreateUserResponse> CreateAsync(CreateUser model);
    Task UpdateRefreshTokenAsync(ApplicationUser user, String refreshToken, DateTime accessTokenDate, Int32 addOnAccessTokenDate);
    Task UpdatePasswordAsync(String userId, String resetToken, String newPassword);
    Task<List<ListUser>> GetAllUsersAsync(Int32 page, Int32 size);
    Int32 TotalUsersCount { get; }
    Task AssignRoleToUserAsync(String id, String[] roles);
    Task<String[]> GetRolesToUserAsync(String id);
}