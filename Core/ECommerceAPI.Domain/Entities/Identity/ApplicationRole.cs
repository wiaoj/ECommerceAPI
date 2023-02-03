using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<String> {
    public ICollection<Endpoint> Endpoints { get; set; }
}