using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using ECommerceAPI.Application.Repositories.Orders;
using System.Runtime.CompilerServices;

namespace ECommerceAPI.Persistence.Services;
public class OrderService : IOrderService {

    private readonly IOrderWriteRepository _orderWriteRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository) {
        _orderWriteRepository = orderWriteRepository;
    }

    public async Task CreateOrderAsync(CreateOrder createOrder) {
        await _orderWriteRepository.AddAsync(new() {
            Id = createOrder.BasketId,
            Address = createOrder.Address,
            Description = createOrder.Description,
        });

        await _orderWriteRepository.SaveAsync();
    }
}