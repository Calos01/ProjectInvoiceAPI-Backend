using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Container
{
    public interface IRepositoryCustomer
    {
        public Task<List<TblCustomer>> GetAll();
        public Task<CustomerDTO> GetById(string Code);
    }
}
