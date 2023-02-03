using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Domain.Entities;
public class Endpoint : BaseEntity {
    public String Code { get; set; }
    public String ActionType { get; set; }
    public String HttpType { get; set; }
    public String Definition { get; set; }
    public MenuEndpoint MenuEndpoint { get; set; }
    public ICollection<ApplicationRole> ApplicationRoles { get; set; }

    public Endpoint() {
        ApplicationRoles = new HashSet<ApplicationRole>();
    }
}

public class MenuEndpoint : BaseEntity {
    public String Name { get; set; }
    public ICollection<Endpoint> Endpoints { get; set; }
}