using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using ECommerceAPI.Application.Repositories.ComplatedOrders;
using ECommerceAPI.Application.Repositories.Orders;
using ECommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services;
public class OrderService : IOrderService {

    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IComplatedOrderWriteRepository _complatedOrderWriteRepository;
    private readonly IComplatedOrderReadRepository _complatedOrderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository,
                        IOrderReadRepository orderReadRepository,
                        IComplatedOrderWriteRepository complatedOrderWriteRepository,
                        IComplatedOrderReadRepository complatedOrderReadRepository) {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _complatedOrderWriteRepository = complatedOrderWriteRepository;
        _complatedOrderReadRepository = complatedOrderReadRepository;
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

    public async Task<ListOrder> GetAllOrdersAsync(Int32 page, Int32 size) {
        var query = _orderReadRepository.Table
              //.Take((page * size)..size)
              .Include(order => order.Basket)
              .ThenInclude(basket => basket.ApplicationUser)
              .Include(order => order.Basket)
                  .ThenInclude(basket => basket.BasketItems)
                  .ThenInclude(basketItem => basketItem.Product);



        var data = query.OrderBy(x => x.CreatedDate)
             .Skip(page * size).Take(size);

        var data2 = from order in data
                    join complatedOrder in _complatedOrderReadRepository.Table
                      on order.Id equals complatedOrder.OrderId into co
                    from _co in co.DefaultIfEmpty()
                    select new {
                        Id = order.Id,
                        OrderCode = order.OrderCode,
                        CreatedDate = order.CreatedDate,
                        Basket = order.Basket,
                        Completed = _co != null ? true : false

                    };

        return new() {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data2.Select(order => new {
                Id = order.Id,
                OrderCode = order.OrderCode,
                UserName = order.Basket.ApplicationUser.UserName,
                TotalPrice = order.Basket.BasketItems.Sum(basketItem => basketItem.Product.Price * basketItem.Quantity),
                CreatedDate = order.CreatedDate,
                Completed = order.Completed,
            }).ToListAsync()
        };
    }

    public async Task<SingleOrder> GetByIdOrderAsync(Guid id) {
        var data = _orderReadRepository.Table
            .Include(order => order.Basket)
                .ThenInclude(basket => basket.BasketItems)
                    .ThenInclude(basketItem => basketItem.Product);

        var data2 = await (from order in data
                     join completedOrder in _complatedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                     from _co in co.DefaultIfEmpty()
                     select new {
                         Id = order.Id,
                         OrderCode = order.OrderCode,
                         CreatedDate = order.CreatedDate,
                         Basket = order.Basket,
                         Completed = _co != null ? true : false,
                         Address = order.Address,
                         Description = order.Description,
                     })
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));

        return new() {
            Id = data2.Id,
            OrderCode = data2.OrderCode,
            Address = data2.Address,
            Description = data2.Description,
            CreatedDate = data2.CreatedDate,
            BasketItems = data2.Basket.BasketItems.Select(basketItem => new {
                basketItem.Product.Name,
                basketItem.Product.Price,
                basketItem.Quantity
            }),
            Completed = data2.Completed
        };
    }

    private String generateOrderCode() {
        Random random = new();
        String orderCode = $"{random.NextDouble()}{random.NextDouble()}";

        orderCode = orderCode.Replace(",", "")
            .Replace(".", "");

        return orderCode[1..^1];
    }

    public async Task CompleteOrderAsync(Guid id) {
        Order order = await _orderReadRepository.GetByIdAsync(id);
        if(order is not null) {
            await _complatedOrderWriteRepository.AddAsync(new() { OrderId = id });
            await _complatedOrderWriteRepository.SaveAsync();
        }
    }
}