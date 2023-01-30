using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Orders.GetOrderById;

public class GetByIdOrderQueryRequest : IRequest<GetByIdOrderQueryResponse> {
    public Guid Id { get; set; }
}