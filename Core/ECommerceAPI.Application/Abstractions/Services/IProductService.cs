namespace ECommerceAPI.Application.Abstractions.Services;
public interface IProductService {
    Task<Byte[]> QrCodeToProductAsync(Guid productId);
    Task StockUpdateToProductAsync(Guid productId, Int16 stock);
}
