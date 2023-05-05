using ProjectInvoiceAPI_Backend.DTO;

namespace ProjectInvoiceAPI_Backend.Container
{
    public interface IRepositoryInvoice
    {
        public Task<List<InvoiceHeaderDTO>> GetAllInvoice();
    }
}
