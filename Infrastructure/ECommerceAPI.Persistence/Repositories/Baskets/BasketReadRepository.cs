using ECommerceAPI.Application.Repositories.Baskets;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Baskets;
public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository {
    public BasketReadRepository(ECommerceAPIDbContext context) : base(context) { }
}