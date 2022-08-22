using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Application.Repositories;
public interface IRepository<TypeEntity> where TypeEntity : class, IBaseEntity, new() {
	DbSet<TypeEntity> Table { get; }
}