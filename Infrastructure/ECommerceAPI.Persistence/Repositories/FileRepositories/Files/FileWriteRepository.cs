using ECommerceAPI.Application.Repositories.FileRepositories.Files;
using ECommerceAPI.Persistence.Contexts;
using File = ECommerceAPI.Domain.Entities.Files.File;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories.Files;

public class FileWriteRepository : WriteRepository<File>, IFileWriteRepository {
    public FileWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}