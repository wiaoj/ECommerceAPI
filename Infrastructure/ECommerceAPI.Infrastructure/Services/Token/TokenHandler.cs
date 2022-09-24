using ECommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services.Token;
public class TokenHandler : ITokenHandler {
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration) {
        _configuration = configuration;
    }

    public Application.DTOs.Token CreateAccessToken() {
        Application.DTOs.Token token = new();

        // SecurityKey 'in simetriğini alıyoruz
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        // Şifrelenmiş kimliği oluşturuyoruz
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Oluşturulacak token ayarlarını veriyoruz
        token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Token:ExpirationMinutes"]));

        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow, // Token üretilir üretilmez devreye giriyor
            signingCredentials: signingCredentials
            );

        // Token oluşturucu sınıfından bir örnek alıyoruz
        JwtSecurityTokenHandler tokenHandler = new();

        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}