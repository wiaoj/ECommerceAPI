using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Products;
public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository {
	public ProductReadRepository(ECommerceAPIDbContext context) : base(context) { }
}