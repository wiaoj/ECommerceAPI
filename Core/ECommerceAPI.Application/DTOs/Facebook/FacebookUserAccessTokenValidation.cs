using System.Text.Json.Serialization;

namespace ECommerceAPI.Application.DTOs.Facebook;

public class FacebookUserAccessTokenValidation {
    [JsonPropertyName("data")]
    public FacebookUserAccessTokenValidationData Data { get; set; }

}

public class FacebookUserAccessTokenValidationData {
    [JsonPropertyName("is_valid")]
    public Boolean IsValid { get; set; }
    [JsonPropertyName("user_id")]
    public String UserId { get; set; }
}