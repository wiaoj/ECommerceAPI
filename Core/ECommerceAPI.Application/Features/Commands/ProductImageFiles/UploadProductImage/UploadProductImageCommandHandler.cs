using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.FileRepositories.ProductImageFiles;
using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Files;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.UploadProductImage;

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse> {
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository) {
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken) {
        List<(String fileName, String pathOrContainer)> datas = await _storageService.UploadAsync("photo-images", request.FormFiles);

        Product product = await _productReadRepository.GetByIdAsync(request.Id);
        await _productImageFileWriteRepository.AddRangeAsync(
            datas.Select(x => new ProductImageFile {
                FileName = x.fileName,
                Path = x.pathOrContainer,
                Storage = _storageService.StorageName,
                Products = new List<Product> { product }
            }).ToList());

        await _productImageFileWriteRepository.SaveAsync();
        return new() { };
    }
}
