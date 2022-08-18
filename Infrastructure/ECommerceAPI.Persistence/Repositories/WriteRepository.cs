using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceAPI.Persistence.Repositories;

public class WriteRepository<TypeEntity> : IWriteRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	private readonly ECommerceAPIDbContext _context;
	public WriteRepository(ECommerceAPIDbContext context) => this._context = context;

	public DbSet<TypeEntity> Table => this._context.Set<TypeEntity>();

	public async Task<Boolean> AddAsync(TypeEntity entity) {
		EntityEntry<TypeEntity> entityEntry = await Table.AddAsync(entity);
		return entityEntry.State.Equals(EntityState.Added);
	}

	public async Task<Boolean> AddRangeAsync(ICollection<TypeEntity> entities) {
		await Table.AddRangeAsync(entities);
		return true;
	}

	public async Task<Boolean> DeleteAsync(TypeEntity entity) {
		EntityEntry<TypeEntity> entityEntry = await Task.Run(() => Table.Remove(entity));
		return entityEntry.State.Equals(EntityState.Deleted);
	}
	public async Task<Boolean> DeleteRangeAsync(ICollection<TypeEntity> entities) {
		await Task.Run(() => Table.RemoveRange(entities));
		return true;
	}

	public async Task<Boolean> DeleteAsync(Guid id) {
		var entity = await Table.FindAsync(id);
		return await DeleteAsync(entity);
	}

	public async Task<Boolean> UpdateAsync(TypeEntity entity) {
		EntityEntry entityEntry = await Task.Run(() => Table.Update(entity));
		return entityEntry.State.Equals(EntityState.Modified);
	}

	public async Task<Boolean> UpdateRangeAsync(TypeEntity entity) {
		await Task.Run(() => Table.UpdateRange(entity));
		return true;
	}

	public async Task<Int32> SaveAsync() {
		//unit of work olduğu için sonradan diğer metodların içinde çağırılacak
		return await _context.SaveChangesAsync();
	}
}