using ECommerceAPI.Application.Abstractions.Services.Authentications;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IAuthService : IInternallAuthentication, IExternalAuthentication { 
    public Task PasswordResetAsync(String email);
    public Task<(Boolean, String)> VerifyResetTokenAsync(String userId, String resetToken);
}