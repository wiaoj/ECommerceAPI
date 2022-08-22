using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.Persistence;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext> {
	public ECommerceAPIDbContext CreateDbContext(String[] args) {
		DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextOptionsBuilder = new();
		dbContextOptionsBuilder.UseNpgsql(Configuration.GetPostgreSQLConnectionString);
		return new(dbContextOptionsBuilder.Options);
	}
}