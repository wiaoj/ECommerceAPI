using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services.Token;
public class TokenHandler : ITokenHandler {
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration) {
        _configuration = configuration;
    }


    public Application.DTOs.Token CreateAccessToken(ApplicationUser applicationUser) {
        return CreateAccessToken(applicationUser, Convert.ToInt32(_configuration["Token:ExpirationSeconds"]));
    }

    public Application.DTOs.Token CreateAccessToken(ApplicationUser applicationUser, Int32 accessTokenLifeTime) {
        Application.DTOs.Token token = new();

        // SecurityKey 'in simetriğini alıyoruz
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        // Şifrelenmiş kimliği oluşturuyoruz
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Oluşturulacak token ayarlarını veriyoruz
        token.Expiration = DateTime.UtcNow.AddSeconds(accessTokenLifeTime);

        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow, // Token üretilir üretilmez devreye giriyor
            signingCredentials: signingCredentials,
            claims: new List<Claim> {
                new(ClaimTypes.Name, applicationUser.UserName)
            }
            );

        // Token oluşturucu sınıfından bir örnek alıyoruz
        JwtSecurityTokenHandler tokenHandler = new();

        token.AccessToken = tokenHandler.WriteToken(securityToken);

        token.RefreshToken = CreateRefreshToken();
        return token;
    }

    public String CreateRefreshToken() {
        Byte[] numbers = new Byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(numbers);
        return Convert.ToBase64String(numbers);
    }
}