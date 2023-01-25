using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity;
public class ApplicationUser : IdentityUser<String> {
    public String NameSurname { get; set; }
    public String? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}