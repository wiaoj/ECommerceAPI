using ECommerceAPI.Application.Repositories.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct;

public class CreateProductCommandRequestHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse> {
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandRequestHandler(IProductWriteRepository productWriteRepository) {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken) {
        await _productWriteRepository.AddAsync(new() {
            Name = request.Name,
            Stock = request.Stock,
            Price = request.Price,
        });
        await _productWriteRepository.SaveAsync();
        return new() { };
    }
}