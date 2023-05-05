using Microsoft.EntityFrameworkCore;
using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Container
{
    public class RepositoryCustomer:IRepositoryCustomer
    {
        private readonly InvoiceDbContext _context;
        public RepositoryCustomer(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblCustomer>> GetAll()
        {
            return  await _context.TblCustomers.ToListAsync();
        }

    }
}
