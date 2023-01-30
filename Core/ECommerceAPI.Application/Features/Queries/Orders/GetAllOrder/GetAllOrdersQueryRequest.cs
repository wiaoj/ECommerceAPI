using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetAllOrder;

public class GetAllOrdersQueryRequest: IRequest<GetAllOrdersQueryResponse> {
    public Int32 Page { get; set; } = 0;
    public Int32 Size { get; set; } = 5;
}