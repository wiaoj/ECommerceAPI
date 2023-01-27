using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceAPI.SignalR.HubServices;
public class ProductHubService : IProductHubService {
    private readonly IHubContext<ProductHub> _hubContext;

    public ProductHubService(IHubContext<ProductHub> hubContext) {
        _hubContext = hubContext;
    }

    public async Task ProductAddedMessageAsync(String message) {
        await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
    }
}