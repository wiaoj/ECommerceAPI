using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;

public class Order : BaseEntity {
    //public Guid CustomerId { get; set; }
    public String Address { get; set; }
    public String Description { get; set; }
    public String OrderCode { get; set; }

    //public ICollection<Product> Products { get; set; }
    //public Customer Customer { get; set; }
    public Basket Basket { get; set; }
    public CompletedOrder CompletedOrder { get; set; }
}