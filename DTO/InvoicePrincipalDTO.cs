namespace ProjectInvoiceAPI_Backend.DTO
{
    public class InvoicePrincipaldto
    {
        public InvoiceHeaderDTO? header { get; set; }
        public List<InvoiceDetailsDTO>? details { get; set; }
    }
}
