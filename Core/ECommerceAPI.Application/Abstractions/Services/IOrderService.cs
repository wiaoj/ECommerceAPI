using ECommerceAPI.Application.DTOs.Order;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IOrderService {
    Task CreateOrderAsync(CreateOrder createOrder);
    Task<ListOrder> GetAllOrderAsync(Int32 page, Int32 size);
    Task<SingleOrder> GetByIdOrderAsync(Guid id);
}