namespace ECommerceAPI.Application.Abstractions.Hubs;

public interface IOrderHubService {
    Task OrderAddedMessageAsync(String message);
}