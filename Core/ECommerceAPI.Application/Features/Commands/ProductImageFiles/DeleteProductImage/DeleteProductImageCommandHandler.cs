using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities.Files;
using ECommerceAPI.Domain.Entities;
using MediatR;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.DeleteProductImage;

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeleteProductImageCommandResponse> {
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository) {
        this._productReadRepository = productReadRepository;
        this._productWriteRepository = productWriteRepository;
    }

    public async Task<DeleteProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken) {
        Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id.Equals(request.Id));

        ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id.Equals(request.ImageId));

        if(productImageFile is not null)
            product?.ProductImageFiles.Remove(productImageFile);
        await _productWriteRepository.SaveAsync();

        return new() { };
    }
}