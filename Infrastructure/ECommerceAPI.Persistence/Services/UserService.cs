using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services;
public class UserService : IUserService {
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager) {
        _userManager = userManager;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUser model) {
        IdentityResult result = await _userManager.CreateAsync(new() {
            Id = Guid.NewGuid().ToString(),
            NameSurname = model.NameSurname,
            UserName = model.UserName,
            Email = model.Email,
        }, model.Password);

        CreateUserResponse response = new() { Succeeded = result.Succeeded };

        if(result.Succeeded)
            response.Message = "Success :)";
        else
            foreach(var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}";

        //throw new UserCreateFailedException();
        return response;
    }

    public async Task UpdateRefreshTokenAsync(ApplicationUser user, String refreshToken, DateTime accessTokenDate, Int32 addOnAccessTokenDate) {

        if(user is null)
            throw new NotFoundUserException();

        user.RefreshToken = refreshToken;
        user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
        await _userManager.UpdateAsync(user);
    }

    public async Task UpdatePasswordAsync(String userId, String resetToken, String newPassword) {
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        if(user is not null) {
            resetToken = resetToken.UrlDecode();

            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if(result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException(result.Errors.First().ToString());
        }
    }
}