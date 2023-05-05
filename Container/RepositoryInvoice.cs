using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Container
{
    public class RepositoryInvoice:IRepositoryInvoice
    {
        private readonly InvoiceDbContext _context;
        public RepositoryInvoice(InvoiceDbContext context)
        {
            _context = context;   
        }

        public Task<List<InvoiceHeaderDTO>> GetAllInvoice()
        {
            throw new NotImplementedException();
        }
    }
}
