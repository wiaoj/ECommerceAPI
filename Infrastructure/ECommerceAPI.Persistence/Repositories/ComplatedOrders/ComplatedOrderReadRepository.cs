using ECommerceAPI.Application.Repositories.ComplatedOrders;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ComplatedOrders;

public class ComplatedOrderReadRepository : ReadRepository<CompletedOrder>, IComplatedOrderReadRepository {
    public ComplatedOrderReadRepository(ECommerceAPIDbContext context) : base(context) {
    }
}