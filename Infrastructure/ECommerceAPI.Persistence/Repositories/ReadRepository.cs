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
		return await Task.Run(() => Table);
	}

	public async Task<TypeEntity> GetByIdAsync(Guid id) {
		return await Table.FindAsync(id);
		//return await Table.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
	}

	public async Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression) {
		return await Table.SingleOrDefaultAsync(expression);
	}

	public async Task<IQueryable<TypeEntity>> GetWhereAsync(Expression<Func<TypeEntity, Boolean>> expression) {
		return await Task.Run(() => Table.Where(expression));
	}
}
