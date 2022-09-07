namespace ECommerceAPI.Application.Abstractions.Storage;
public interface IStorageService : IStorage {
    public String StorageName { get; }
}