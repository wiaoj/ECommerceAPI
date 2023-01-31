using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetOrderById;

public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse> {
    private readonly IOrderService _orderService;

    public GetByIdOrderQueryHandler(IOrderService orderService) {
        _orderService = orderService;
    }

    public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken) {
        SingleOrder data = await _orderService.GetByIdOrderAsync(request.Id);

        return new() {
            Id = data.Id,
            OrderCode = data.OrderCode,
            Address = data.Address,
            Description = data.Description,
            BasketItems = data.BasketItems,
            CreatedDate = data.CreatedDate,
            Completed = data.Completed,
        };
    }
}