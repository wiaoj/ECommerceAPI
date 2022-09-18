using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;

public class UpdateProductCommandRequestHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse> {
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandRequestHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository) {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken) {
        Product product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Name = request.Name;
        product.Stock = request.Stock;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        return new() { };
    }
}