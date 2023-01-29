using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Baskets.AddItemToBasket;

public class AddItemToBasketCommandRequest : IRequest<AddItemToBasketCommandResponse> {
    public Guid ProductId { get; set; }
    public Int32 Quantity { get; set; }
}