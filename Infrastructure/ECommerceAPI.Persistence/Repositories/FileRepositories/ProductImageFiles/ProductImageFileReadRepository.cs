using ECommerceAPI.Application.Repositories.FileRepositories.ProductImageFiles;
using ECommerceAPI.Domain.Entities.Files;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories.ProductImageFiles;
public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository {
    public ProductImageFileReadRepository(ECommerceAPIDbContext context) : base(context) { }
}