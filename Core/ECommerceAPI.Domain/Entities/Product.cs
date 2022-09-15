using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Files;

namespace ECommerceAPI.Domain.Entities;
public class Product : BaseEntity {
	public String Name { get; set; }
	public Int16 Stock { get; set; }
	public Decimal Price { get; set; }

	public ICollection<Order> Orders { get; set; }
	public ICollection<ProductImageFile> ProductImageFiles { get; set; }
}