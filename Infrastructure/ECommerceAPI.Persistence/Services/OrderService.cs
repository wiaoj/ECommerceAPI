using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using ECommerceAPI.Application.Repositories.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ECommerceAPI.Persistence.Services;
public class OrderService : IOrderService {

    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository) {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
    }

    public async Task CreateOrderAsync(CreateOrder createOrder) {
        await _orderWriteRepository.AddAsync(new() {
            Id = createOrder.BasketId,
            Address = createOrder.Address,
            Description = createOrder.Description,
            OrderCode = generateOrderCode()
        });

        await _orderWriteRepository.SaveAsync();
    }

    public async Task<ListOrder> GetAllOrderAsync(Int32 page, Int32 size) {
        var query = _orderReadRepository.Table
              //.Take((page * size)..size)
              .Include(order => order.Basket)
              .ThenInclude(basket => basket.ApplicationUser)
              .Include(order => order.Basket)
                  .ThenInclude(basket => basket.BasketItems)
                  .ThenInclude(basketItem => basketItem.Product);

        var data = query.OrderBy(x => x.CreatedDate)
             .Skip(page * size).Take(size);
        //.Select(order => new ListOrder {
        //     OrderCode = order.OrderCode,
        //     UserName = order.Basket.ApplicationUser.UserName,
        //     TotalPrice = order.Basket.BasketItems.Sum(basketItem => basketItem.Product.Price * basketItem.Quantity),
        //     CreatedDate = order.CreatedDate
        // })
        //.ToListAsync();

        return new() {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data.Select(order => new {
                OrderCode = order.OrderCode,
                UserName = order.Basket.ApplicationUser.UserName,
                TotalPrice = order.Basket.BasketItems.Sum(basketItem => basketItem.Product.Price * basketItem.Quantity),
                CreatedDate = order.CreatedDate
            }).ToListAsync()
        };
    }

    private String generateOrderCode() {
        Random random = new();
        String orderCode = $"{random.NextDouble()}{random.NextDouble()}";

        orderCode = orderCode.Replace(",", "")
            .Replace(".", "");

        return orderCode[1..^1];
    }
}