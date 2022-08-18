using ECommerceAPI.Application.Repositories.Orders;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Orders;
public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository {
	public OrderReadRepository(ECommerceAPIDbContext context) : base(context) { }
}