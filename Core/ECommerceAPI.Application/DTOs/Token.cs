namespace ECommerceAPI.Application.DTOs;
public class Token {
    public String AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public String RefreshToken { get; set; }
}