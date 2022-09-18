using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Products.GetByIdProduct;
public class GetByIdProductQeuryRequest : IRequest<GetByIdProductQeuryResponse> {
    public Guid Id { get; set; }
}