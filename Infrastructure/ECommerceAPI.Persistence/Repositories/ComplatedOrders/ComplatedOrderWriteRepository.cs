using ECommerceAPI.Application.Repositories.ComplatedOrders;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ComplatedOrders;
public class ComplatedOrderWriteRepository : WriteRepository<CompletedOrder>, IComplatedOrderWriteRepository {
    public ComplatedOrderWriteRepository(ECommerceAPIDbContext context) : base(context) {
    }
}