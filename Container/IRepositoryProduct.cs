using ProjectInvoiceAPI_Backend.DTO;

namespace ProjectInvoiceAPI_Backend.Container
{
    public interface IRepositoryProduct
    {
        public Task<List<ProductDTO>> GetAllProducts();
        public Task<ProductDTO> GetProductById(string code);
        public Task<List<ProductDTO>> GetProductByCategory(int cat);
    }
}
