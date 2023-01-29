using ECommerceAPI.Application.DTOs.Order;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IOrderService {
    Task CreateOrderAsync(CreateOrder createOrder);
}