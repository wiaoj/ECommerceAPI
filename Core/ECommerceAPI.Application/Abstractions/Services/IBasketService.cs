using ECommerceAPI.Application.ViewModels.Baskets;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IBasketService {
    public Task<List<BasketItem>> GetBasketItemsAsync();
    public Task AddItemToBasketAsync(VievModel_Create_BasketItem basketItem);
    public Task UpdateQuantityAsync(VievModel_Update_BasketItem basketItem);
    public Task RemoveBasketItemAsync(Guid basketItemId);
    public Basket GetUserActiveBasketAsync { get; }
}