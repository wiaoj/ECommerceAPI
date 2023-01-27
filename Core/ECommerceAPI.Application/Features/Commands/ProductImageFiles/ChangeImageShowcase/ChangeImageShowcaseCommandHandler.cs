using ECommerceAPI.Application.Repositories.FileRepositories.ProductImageFiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.ChangeImageShowcase;

public class ChangeImageShowcaseCommandHandler : IRequestHandler<ChangeImageShowcaseCommandRequest, ChangeImageShowcaseCommandResponse> {

    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public ChangeImageShowcaseCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository) {
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<ChangeImageShowcaseCommandResponse> Handle(ChangeImageShowcaseCommandRequest request, CancellationToken cancellationToken) {
        var query = _productImageFileWriteRepository.Table.Include(x => x.Products)
             .SelectMany(x => x.Products, (productsImageFile, products) => new {
                 productsImageFile,
                 products
             });

        var data = await query.FirstOrDefaultAsync(p => p.products.Id.Equals(request.ProductId) && p.productsImageFile.Showcase);

        if(data is not null)
            data.productsImageFile.Showcase = false;

        var image = await query.FirstOrDefaultAsync(p => p.productsImageFile.Id.Equals(request.ImageId));

        if(image is not null)
            image.productsImageFile.Showcase = true;

        await _productImageFileWriteRepository.SaveAsync();

        return new() { };
    }
}