using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Domain.Entities;
public class Basket : BaseEntity {
    public String ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public Order Order { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
}