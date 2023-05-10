using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Container
{
    public class RepositoryCustomer:IRepositoryCustomer
    {
        private readonly InvoiceDbContext _context;
        private readonly IMapper _mapper;
        public RepositoryCustomer(InvoiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public async Task<List<TblCustomer>> GetAll()
        {
            return  await _context.TblCustomers.ToListAsync();
        }

        public async Task<CustomerDTO> GetById(string Code)
        {
            try
            {
                var customerid = await this._context.TblCustomers.FirstOrDefaultAsync(data=>data.Code==Code);
                return this._mapper.Map<CustomerDTO>(customerid);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
