using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Baskets.RemoveBasketItem;

public class RemoveBasketItemCommandRequest : IRequest<RemoveBasketItemCommandResponse> {
    public Guid BasketItemId { get; set; }
}