using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;
public class Product : BaseEntity {
	public String Name { get; set; }
	public Int16 Stock { get; set; }
	public Decimal Price { get; set; }

	public ICollection<Order> Orders { get; set; }
}