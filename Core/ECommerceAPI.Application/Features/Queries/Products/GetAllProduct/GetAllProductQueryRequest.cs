using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Products.GetAllProduct;
public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse> {
    //public Pagination Pagination { get; set; }
    public Int32 Page { get; set; } = 0;
    public Int32 Size { get; set; } = 5;
}