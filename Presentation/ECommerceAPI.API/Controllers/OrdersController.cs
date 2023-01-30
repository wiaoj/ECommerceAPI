using ECommerceAPI.Application.Features.Commands.Orders.CreateOrderCommand;
using ECommerceAPI.Application.Features.Queries.Orders.GetAllOrder;
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
    public async Task<ActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest) {
        CreateOrderCommandResponse response = await _sender.Send(createOrderCommandRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest) {
        GetAllOrdersQueryResponse response = await _sender.Send(getAllOrdersQueryRequest);
        return Ok(response);
    }
}