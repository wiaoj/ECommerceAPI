using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Baskets.UpdateQuantity;

public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse> {
    public Guid BasketItemId { get; set; }
    public Int32 Quantity { get; set; }
}