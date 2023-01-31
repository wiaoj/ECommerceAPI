using ECommerceAPI.Application.DTOs.Order;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IOrderService {
    Task CreateOrderAsync(CreateOrder createOrder);
    Task<ListOrder> GetAllOrdersAsync(Int32 page, Int32 size);
    Task<SingleOrder> GetByIdOrderAsync(Guid id);
    Task<(Boolean, CompletedOrderDto)> CompleteOrderAsync(Guid id);
}