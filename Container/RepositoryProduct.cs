using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Container
{
    public class RepositoryProduct : IRepositoryProduct
    {
        private readonly InvoiceDbContext _context;
        private readonly IMapper _mapper;
        public RepositoryProduct(InvoiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _context.TblProducts.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<List<ProductDTO>> GetProductByCategory(int cat)
        {
            var listcategory=await _context.TblProducts.Where(data=>data.Category==cat).ToListAsync();
            return _mapper.Map<List<ProductDTO>>(listcategory);
        }

        public async Task<ProductDTO> GetProductById(string code)
        {
            var productid=await _context.TblProducts.FirstOrDefaultAsync(x => x.Code==code);
            return _mapper.Map<ProductDTO>(productid);
        }
    }
}
