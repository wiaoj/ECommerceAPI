using MediatR;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImages;
public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>?> {
    public Guid Id { get; set; }
}