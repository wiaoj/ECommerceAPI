using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity;
public class ApplicationUser : IdentityUser<Guid> {
    public String NameSurname { get; set; }
}