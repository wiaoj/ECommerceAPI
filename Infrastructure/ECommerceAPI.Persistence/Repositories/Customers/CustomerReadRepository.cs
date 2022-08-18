using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Customers;
public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository {
	public CustomerReadRepository(ECommerceAPIDbContext context) : base(context) { }
}