namespace ProjectInvoiceAPI_Backend.DTO
{
    public class ProductDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Category { get; set; }
    }
}
