using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories.BasketItems;
using ECommerceAPI.Application.Repositories.Baskets;
using ECommerceAPI.Application.Repositories.Orders;
using ECommerceAPI.Application.ViewModels.Baskets;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services;
public class BasketService : IBasketService {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;

    public BasketService(IHttpContextAccessor httpContextAccessor,
                         UserManager<ApplicationUser> userManager,
                         IOrderReadRepository orderReadRepository,
                         IBasketWriteRepository basketWriteRepository,
                         IBasketItemReadRepository basketItemReadRepository,
                         IBasketItemWriteRepository basketItemWriteRepository,
                         IBasketReadRepository basketReadRepository) {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketReadRepository = basketReadRepository;
    }

    private async Task<Basket> ContextUser() {
        String? username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

        if(String.IsNullOrEmpty(username))
            throw new Exception("Beklenmeyen bir hatayla karşılaşıldı");


        ApplicationUser? user = await _userManager.Users.Include(user => user.Baskets)
            .FirstOrDefaultAsync(user => user.UserName.Equals(username));

        var _basket = from basket in user.Baskets
                      join order in _orderReadRepository.Table
                      on basket.Id equals order.Id into basketOrders
                      from order in basketOrders.DefaultIfEmpty()
                      select new {
                          Basket = basket,
                          Order = order,
                      };

        Basket? targetBasket = null;

        if(_basket.Any(basket => basket.Order is null))
            targetBasket = _basket.FirstOrDefault(x => x.Order is null)?.Basket;
        else {
            targetBasket = new();

            user.Baskets.Add(targetBasket);

            await _basketWriteRepository.SaveAsync();
        }


        return targetBasket!;
    }

    public async Task AddItemToBasketAsync(VievModel_Create_BasketItem basketItem) {
        Basket? basket = await ContextUser();

        if(basket is not null) {
            BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(
                 x => x.BasketId.Equals(basket.Id) &&
                 x.ProductId.Equals(basketItem.ProductId)
                 );

            if(_basketItem is not null)
                _basketItem.Quantity++;
            else {
                await _basketItemWriteRepository.AddAsync(new() {
                    BasketId = basket.Id,
                    ProductId = basketItem.ProductId,
                    Quantity = basketItem.Quantity,
                });

            }
            await _basketItemWriteRepository.SaveAsync();

        }
    }

    public async Task<List<BasketItem>> GetBasketItemsAsync() {
        Basket? basket = await ContextUser();

        Basket? basketResult = await _basketReadRepository.Table
            .Include(x => x.BasketItems)
            .ThenInclude(basketItem => basketItem.Product)
            .FirstOrDefaultAsync(x => x.Id.Equals(basket.Id));

        return basketResult.BasketItems.ToList();
    }

    public async Task RemoveBasketItemAsync(Guid basketItemId) {
        await _basketItemWriteRepository.DeleteAsync(basketItemId);
        await _basketItemWriteRepository.SaveAsync();
    }

    public async Task UpdateQuantityAsync(VievModel_Update_BasketItem basketItem) {
        BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);

        if(_basketItem is not null) {
            _basketItem.Quantity = basketItem.Quantity;
            //await _basketItemWriteRepository.UpdateAsync(_basketItem);
            await _basketItemWriteRepository.SaveAsync();
        }
    }
    public Basket GetUserActiveBasketAsync {
        get {
            return ContextUser().Result;
        }
    }
}