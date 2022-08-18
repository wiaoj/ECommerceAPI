using ECommerceAPI.Application.Repositories.Orders;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Orders;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository {
	public OrderWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}