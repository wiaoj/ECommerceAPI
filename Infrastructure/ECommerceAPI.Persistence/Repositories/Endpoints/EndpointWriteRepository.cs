using ECommerceAPI.Application.Repositories.Endpoints;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Endpoints;

public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository {
    public EndpointWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}