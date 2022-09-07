using ECommerceAPI.Application.Repositories.FileRepositories.InvoiceFiles;
using ECommerceAPI.Domain.Entities.Files;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.FileRepositories.InvoiceFiles;

public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository {
    public InvoiceFileWriteRepository(ECommerceAPIDbContext context) : base(context) { }
}