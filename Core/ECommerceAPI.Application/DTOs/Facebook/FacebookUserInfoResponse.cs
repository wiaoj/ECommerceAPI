using System.Text.Json.Serialization;

namespace ECommerceAPI.Application.DTOs.Facebook;

public class FacebookUserInfoResponse {
    [JsonPropertyName("id")]
    public String Id { get; set; }
    [JsonPropertyName("email")]
    public String Email { get; set; }
    [JsonPropertyName("name")]
    public String Name { get; set; }
}