using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;

public class Customer : BaseEntity {
	public String Name { get; set; }

	public ICollection<Order> Orders { get; set; }
}