using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
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
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Baskets,
        ActionType = ActionType.Reading,
        Definition = "Get Basket Items"
        )]
    public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest) {
        List<GetBasketItemsQueryResponse> response = await _sender.Send(getBasketItemsQueryRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Baskets,
        ActionType = ActionType.Writing,
        Definition = "Add Item To Basket"
        )]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest) {
        AddItemToBasketCommandResponse response = await _sender.Send(addItemToBasketCommandRequest);
        return Ok(response);
    }

    [HttpPut("[action]")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Baskets,
        ActionType = ActionType.Updating,
        Definition = "Update Quantity"
        )]
    public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest) {
        UpdateQuantityCommandResponse response = await _sender.Send(updateQuantityCommandRequest);
        return Ok(response);
    }

    [HttpDelete("[action]/{BasketItemId:Guid}")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Baskets,
        ActionType = ActionType.Deleting,
        Definition = "Remove Basket Item"
        )]
    public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest) {
        RemoveBasketItemCommandResponse response = await _sender.Send(removeBasketItemCommandRequest);
        return Ok(response);
    }
}