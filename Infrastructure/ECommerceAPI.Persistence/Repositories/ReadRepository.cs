using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories;

public class ReadRepository<TypeEntity> : IReadRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	private readonly ECommerceAPIDbContext _context;
	public ReadRepository(ECommerceAPIDbContext context) => this._context = context;

	public DbSet<TypeEntity> Table => _context.Set<TypeEntity>();

	public async Task<IQueryable<TypeEntity>> GetAll() {
		return await GetAll(true);
	}

	public async Task<IQueryable<TypeEntity>> GetAll(Boolean tracking) {
		return await Task.Run(() => {
			var query = Table.AsQueryable();
			return tracking ? query : query.AsNoTracking();
		});
	}

	public async Task<TypeEntity> GetByIdAsync(Guid id) {
		return await GetByIdAsync(id, true);
	}

	public async Task<TypeEntity> GetByIdAsync(Guid id, Boolean tracking) {
		//IQueryable da findasync metodu yoktur marker pattern ile id üzerinden işlem yapılır
		//return await Table.FindAsync(id);
		//return await Table.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
		return await Task.Run(() => {
			var query = Table.AsQueryable();
			return tracking ?
				query.FirstOrDefaultAsync(entity => entity.Id.Equals(id)) :
				query.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
		});
	}

	public async Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression) {
		return await GetSingleAsync(expression, true);
	}

	public async Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking) {
		return await Task.Run(() => {
			var query = Table.AsQueryable();
			return tracking ?
				query.SingleOrDefaultAsync(expression) :
				query.AsNoTracking().SingleOrDefaultAsync(expression);
		});
	}

	public async Task<IQueryable<TypeEntity>> GetWhereAsync(Expression<Func<TypeEntity, Boolean>> expression) {
		return await GetWhereAsync(expression, true);
	}

	public async Task<IQueryable<TypeEntity>> GetWhereAsync(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking) {
		//return await Task.Run(() => Table.Where(expression));
		return await Task.Run(() => {
			var query = Table.Where(expression);
			return tracking ? query : query.AsNoTracking();
		});
	}
}
