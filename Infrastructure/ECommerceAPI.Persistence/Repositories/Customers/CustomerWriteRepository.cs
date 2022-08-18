using ECommerceAPI.Application.Repositories.Customers;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Customers;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository {
	public CustomerWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}