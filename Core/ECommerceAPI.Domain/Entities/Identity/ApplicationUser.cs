using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity;
public class ApplicationUser : IdentityUser<String> {
    public String NameSurname { get; set; }
}