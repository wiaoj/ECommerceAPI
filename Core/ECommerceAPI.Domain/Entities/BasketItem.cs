using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;

public class BasketItem : BaseEntity {
    public Guid BasketId { get; set; }
    Basket Basket { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public Int32 Quantity { get; set; }
}