using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Orders.ComplateOrderCommand;
using ECommerceAPI.Application.Features.Commands.Orders.CreateOrderCommand;
using ECommerceAPI.Application.Features.Queries.Orders.GetAllOrder;
using ECommerceAPI.Application.Features.Queries.Orders.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class OrdersController : ControllerBase {

    private readonly ISender _sender;

    public OrdersController(ISender sender) {
        _sender = sender;
    }

    [HttpPost("[action]")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Orders,
        ActionType = ActionType.Writing,
        Definition = "Create Order"
        )]
    public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest) {
        CreateOrderCommandResponse response = await _sender.Send(createOrderCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Orders,
        ActionType = ActionType.Reading,
        Definition = "Get All Orders"
        )]
    public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest) {
        GetAllOrdersQueryResponse response = await _sender.Send(getAllOrdersQueryRequest);
        return Ok(response);
    }

    [HttpGet("{Id}")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Orders,
        ActionType = ActionType.Reading,
        Definition = "Get Order By Id"
        )]
    public async Task<IActionResult> GetByIdOrder([FromRoute] GetByIdOrderQueryRequest getAllOrdersQueryRequest) {
        GetByIdOrderQueryResponse response = await _sender.Send(getAllOrdersQueryRequest);
        return Ok(response);
    }

    [HttpGet("complete-order/{Id}")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Orders,
        ActionType = ActionType.Updating,
        Definition = "Complete Order"
        )]
    public async Task<IActionResult> CompleteOrder([FromRoute] ComplateOrderCommandRequest complateOrderCommandRequest) {
        ComplateOrderCommandResponse response = await _sender.Send(complateOrderCommandRequest);
        return Ok(response);
    }
}