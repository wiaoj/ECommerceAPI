using ECommerceAPI.Application.Repositories.BasketItems;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.BasketItems;
public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository {
    public BasketItemReadRepository(ECommerceAPIDbContext context) : base(context) { }
}