using System.Text.Json.Serialization;

namespace ECommerceAPI.Application.DTOs.Facebook;
public class FacebookAccessTokenResponse {
    [JsonPropertyName("access_token")]
    public String AccessToken { get; set; }
    [JsonPropertyName("token_type")] // Veriyi hangi isimden aldığımızı belirtiyoruz
    public String TokenType { get; set; }
}