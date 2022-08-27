using ECommerceAPI.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories;

public interface IReadRepository<TypeEntity> : IRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	Task<IQueryable<TypeEntity>> GetAll();
	Task<IQueryable<TypeEntity>> GetAll(Boolean tracking);
	//tracking default= true tracke edilerek gelmesini istiyorsak default olarak true değeri veriyoruz, data tracke edilerek geliyor (tracke etmek => datayı takip etmek)
	//tracke edilen data değiştirilince veritabanına fiziksel olarak yansımaz
	Task<IQueryable<TypeEntity>> GetWhereAsync(Expression<Func<TypeEntity, Boolean>> expression);
	Task<IQueryable<TypeEntity>> GetWhereAsync(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking);
	Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression);
	Task<TypeEntity> GetSingleAsync(Expression<Func<TypeEntity, Boolean>> expression, Boolean tracking);
	Task<TypeEntity> GetByIdAsync(Guid id);
	Task<TypeEntity> GetByIdAsync(Guid id, Boolean tracking);
}