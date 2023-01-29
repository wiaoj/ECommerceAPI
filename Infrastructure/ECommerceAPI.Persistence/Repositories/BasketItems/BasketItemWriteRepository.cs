using ECommerceAPI.Application.Repositories.BasketItems;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.BasketItems;
public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository {
    public BasketItemWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}