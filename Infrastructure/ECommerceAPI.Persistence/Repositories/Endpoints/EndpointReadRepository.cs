using ECommerceAPI.Application.Repositories.Endpoints;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Endpoints;

public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository {
    public EndpointReadRepository(ECommerceAPIDbContext context) : base(context) { }
}