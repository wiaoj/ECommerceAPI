using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories;
public interface IRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	DbSet<TypeEntity> Table { get; }
}

public interface IReadRepository<TypeEntity> : IRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	Task<IQueryable<TypeEntity>> GetAll();
	Task<IQueryable<TypeEntity>> GetWhereAsync(Expression<Func<TypeEntity, Boolean>> expression);
	Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression);
	Task<TypeEntity> GetByIdAsync(Guid id);
}

public interface IWriteRepository<TypeEntity> : IRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	Task<Boolean> AddAsync(TypeEntity entity);
	Task<Boolean> AddRangeAsync(ICollection<TypeEntity> entities);
	Task<Boolean> DeleteAsync(TypeEntity entity);
	Task<Boolean> DeleteRangeAsync(ICollection<TypeEntity> entities);
	Task<Boolean> DeleteAsync(Guid id);
	Task<Boolean> UpdateAsync(TypeEntity entity);
	Task<Int32> SaveAsync();
}