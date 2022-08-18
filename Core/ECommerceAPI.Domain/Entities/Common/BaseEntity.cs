using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Domain.Entities.Common;
public abstract class BaseEntity : IBaseEntity {
	public Guid Id { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }

}

public interface IBaseEntity : IEntity<Guid> { }
public interface IEntity<EntityIdType> where EntityIdType : struct {
	public EntityIdType Id { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}