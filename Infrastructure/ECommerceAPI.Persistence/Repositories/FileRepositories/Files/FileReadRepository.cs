using ECommerceAPI.Application.Repositories.FileRepositories.Files;
using ECommerceAPI.Persistence.Contexts;
using File = ECommerceAPI.Domain.Entities.Files.File;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories.Files;
public class FileReadRepository : ReadRepository<File>, IFileReadRepository {
    public FileReadRepository(ECommerceAPIDbContext context) : base(context) { }
}