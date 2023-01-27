namespace ECommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImages;

public class GetProductImagesQueryResponse {
    public Guid? Id { get; set; }
    public String? FileName { get; set; }
    public String? Path { get; set; }
    public Boolean Showcase { get; set; }
}