using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.UploadProductImage;

public class UploadProductImageCommandRequest : IRequest<UploadProductImageCommandResponse> {
    public Guid Id { get; set; }
    public IFormFileCollection? FormFiles { get; set; }
}