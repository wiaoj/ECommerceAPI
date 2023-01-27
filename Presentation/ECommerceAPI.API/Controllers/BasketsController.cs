using ECommerceAPI.Application.Features.Commands.Baskets.AddItemToBasket;
using ECommerceAPI.Application.Features.Commands.Baskets.RemoveBasketItem;
using ECommerceAPI.Application.Features.Commands.Baskets.UpdateQuantity;
using ECommerceAPI.Application.Features.Queries.Baskets.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class BasketsController : ControllerBase {
    private readonly ISender _sender;

    public BasketsController(ISender sender) {
        _sender = sender;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest) {
        List<GetBasketItemsQueryResponse> response = await _sender.Send(getBasketItemsQueryRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest) {
        AddItemToBasketCommandResponse response = await _sender.Send(addItemToBasketCommandRequest);
        return Ok(response);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest) {
        UpdateQuantityCommandResponse response = await _sender.Send(updateQuantityCommandRequest);
        return Ok(response);
    }

    [HttpDelete("[action]/{BasketItemId:Guid}")]
    public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest) {
        RemoveBasketItemCommandResponse response = await _sender.Send(removeBasketItemCommandRequest);
        return Ok(response);
    }
}