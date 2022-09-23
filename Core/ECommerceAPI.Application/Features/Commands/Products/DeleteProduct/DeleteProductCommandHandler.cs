using ECommerceAPI.Application.Repositories.Products;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse> {
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository) {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken) {
        await _productWriteRepository.DeleteAsync(request.Id);
        await _productWriteRepository.SaveAsync();
        return new() { };
    }
}