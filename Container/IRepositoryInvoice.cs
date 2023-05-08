using ProjectInvoiceAPI_Backend.DTO;

namespace ProjectInvoiceAPI_Backend.Container
{
    public interface IRepositoryInvoice
    {
        public Task<List<InvoiceHeaderDTO>> GetAllInvoiceHeader();
        public Task<InvoiceHeaderDTO> GetInvoiceHeaderPorId(string invoiceno);
        public Task<List<InvoiceDetailsDTO>> GetInvoiceDetails(string invoice);

        public Task<InvoiceRespuestaDTO> Save(InvoicePrincipaldto invoice);
        public Task<InvoiceRespuestaDTO> Delete(string invoiceno);
    }
}
