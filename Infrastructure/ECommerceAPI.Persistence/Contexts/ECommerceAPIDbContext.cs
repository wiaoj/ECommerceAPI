using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Files;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = ECommerceAPI.Domain.Entities.Files.File;

namespace ECommerceAPI.Persistence.Contexts;
public class ECommerceAPIDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, String> {
	public ECommerceAPIDbContext(DbContextOptions options) : base(options) { }


	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<File> Files { get; set; }
	public DbSet<ProductImageFile> ProductImageFiles { get; set; }
	public DbSet<InvoiceFile> InvoiceFiles { get; set; }

	public override async Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default) {
		//ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.
		var datas = ChangeTracker.Entries<BaseEntity>();

		foreach(var data in datas) {
			_ = data.State switch {
				EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
				EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
				_ => DateTime.UtcNow,
			};
		}

		return await base.SaveChangesAsync(cancellationToken);
	}
}