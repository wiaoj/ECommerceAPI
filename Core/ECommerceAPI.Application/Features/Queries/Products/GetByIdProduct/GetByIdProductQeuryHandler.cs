using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQeuryHandler : IRequestHandler<GetByIdProductQeuryRequest, GetByIdProductQeuryResponse> {
    private readonly IProductReadRepository _productReadRepository;

    public GetByIdProductQeuryHandler(IProductReadRepository productReadRepository) {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetByIdProductQeuryResponse> Handle(GetByIdProductQeuryRequest request, CancellationToken cancellationToken) {
        Product product = await _productReadRepository.GetByIdAsync(request.Id, tracking: false);
        return new() {
            Id = product.Id,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate,
            Name = product.Name,
            Stock = product.Stock,
            Price = product.Price
        };
    }
}