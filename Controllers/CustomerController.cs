using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectInvoiceAPI_Backend.Container;
using ProjectInvoiceAPI_Backend.DTO;

namespace ProjectInvoiceAPI_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly IRepositoryCustomer _repoContext;
        private readonly IMapper _mapper;
        public CustomerController(IRepositoryCustomer repoContext, IMapper mapper)
        {
            _repoContext = repoContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var mascota = await _repoContext.GetAll();
                if (mascota == null)
                {
                    return NotFound();
                }
                var mascotaDto=_mapper.Map<List<CustomerDTO>>(mascota);

                return Ok(mascotaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCustomer")]
        public async Task<CustomerDTO> ObtenerCustomerId(string code)
        {
            return await _repoContext.GetById(code);
        }

    }
}
