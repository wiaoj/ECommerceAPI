using ECommerceAPI.Application.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse> {
    readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository) {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken) {
        var totalCount = _productReadRepository.GetAll(tracking: false).Count();
        var products = await _productReadRepository.GetAll(tracking: false)
            .Skip(request.Page * request.Size).Take(request.Size)
            .Select(p => new {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToListAsync(cancellationToken: cancellationToken);

        return new() {
            Products = products,
            TotalCount = totalCount
        };
    }
}
