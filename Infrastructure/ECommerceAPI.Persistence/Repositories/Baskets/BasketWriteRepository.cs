using ECommerceAPI.Application.Repositories.Baskets;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Baskets;
public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository {
    public BasketWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}