using ECommerceAPI.Application.Repositories.MenuEndpoints;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.MenuEndpoints;
public class MenuEndpointReadRepository : ReadRepository<MenuEndpoint>, IMenuEndpointReadRepository {
    public MenuEndpointReadRepository(ECommerceAPIDbContext context) : base(context) { }
}