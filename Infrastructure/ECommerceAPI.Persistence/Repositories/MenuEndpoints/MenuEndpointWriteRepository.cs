using ECommerceAPI.Application.Repositories.MenuEndpoints;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.MenuEndpoints;

public class MenuEndpointWriteRepository : WriteRepository<MenuEndpoint>, IMenuEndpointWriteRepository {
    public MenuEndpointWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}