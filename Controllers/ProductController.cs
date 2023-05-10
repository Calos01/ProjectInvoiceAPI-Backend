using Microsoft.AspNetCore.Mvc;
using ProjectInvoiceAPI_Backend.Container;
using ProjectInvoiceAPI_Backend.DTO;

namespace ProjectInvoiceAPI_Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryProduct _repoProd;
        public ProductController(IRepositoryProduct repoProd)
        {
            _repoProd = repoProd;
        }
        [HttpGet("GetProducts")]
        public async Task<List<ProductDTO>> ObtenerProducts()
        {
            return await _repoProd.GetAllProducts();
        }
        [HttpGet("GetProductId")]
        public async Task<ProductDTO> ObtenerProductId(string code)
        {
            return await _repoProd.GetProductById(code);
        }
        [HttpGet("GetProductCategory")]
        public async Task<List<ProductDTO>> ObtenerProductCategory(int cat)
        {
            return await _repoProd.GetProductByCategory(cat);
        }
    }
}
