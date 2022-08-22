using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Application.Repositories;

public interface IWriteRepository<TypeEntity> : IRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	Task<Boolean> AddAsync(TypeEntity entity);
	Task<Boolean> AddRangeAsync(ICollection<TypeEntity> entities);
	Task<Boolean> DeleteAsync(TypeEntity entity);
	Task<Boolean> DeleteRangeAsync(ICollection<TypeEntity> entities);
	Task<Boolean> DeleteAsync(Guid id);
	Task<Boolean> UpdateAsync(TypeEntity entity);
	Task<Int32> SaveAsync();
}