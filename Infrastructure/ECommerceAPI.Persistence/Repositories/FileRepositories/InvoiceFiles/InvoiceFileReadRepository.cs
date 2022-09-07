using ECommerceAPI.Application.Repositories.FileRepositories.InvoiceFiles;
using ECommerceAPI.Domain.Entities.Files;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories.InvoiceFiles;
public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository {
    public InvoiceFileReadRepository(ECommerceAPIDbContext context) : base(context) { }
}