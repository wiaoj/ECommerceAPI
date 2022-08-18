using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Products;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository {
	public ProductWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}