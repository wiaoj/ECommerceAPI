using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>?> {
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration _configuration;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration) {
        _productReadRepository = productReadRepository;
        _configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>?> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken) {
        Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id.Equals(request.Id));

        return product?.ProductImageFiles.Select(x => new GetProductImagesQueryResponse {
            Id = x.Id,
            FileName = x.FileName,
            Path = $"{_configuration["Storage:BaseUrl"]}/{x.Path}"
        }).ToList();
    }
}