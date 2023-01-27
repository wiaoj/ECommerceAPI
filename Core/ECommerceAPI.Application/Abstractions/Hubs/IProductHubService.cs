namespace ECommerceAPI.Application.Abstractions.Hubs;
public interface IProductHubService {
    Task ProductAddedMessageAsync(String message);
}