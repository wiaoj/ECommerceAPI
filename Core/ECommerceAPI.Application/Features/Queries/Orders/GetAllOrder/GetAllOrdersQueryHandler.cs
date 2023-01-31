using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetAllOrder;
public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse> {
    private readonly IOrderService _orderService;

    public GetAllOrdersQueryHandler(IOrderService orderService) {
        _orderService = orderService;
    }

    public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken) {
        ListOrder data = await _orderService.GetAllOrdersAsync(request.Page, request.Size);

        return new() {
            TotalOrderCount = data.TotalOrderCount,
            Orders = data.Orders
        };
        //return data.ConvertAll(order => new GetAllOrdersQueryResponse {
        //    OrderCode = order.OrderCode,
        //    UserName = order.UserName,
        //    TotalPrice = order.TotalPrice,
        //    CreatedDate = order.CreatedDate,
        //});
    }
}