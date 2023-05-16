using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectInvoiceAPI_Backend.Container;
using ProjectInvoiceAPI_Backend.DTO;

namespace ProjectInvoiceAPI_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IRepositoryInvoice _repoContext;
        public InvoiceController(IRepositoryInvoice repo)
        {
            _repoContext = repo;
        }

        [HttpGet("GetInvoices")]
        public async Task<List<InvoiceHeaderDTO>> ObtenerListaInvoice()
        {
            return await _repoContext.GetAllInvoiceHeader();
        }
        [HttpGet("GetInvoiceId")]
        public async Task<InvoiceHeaderDTO> ObtenerInvoiceId(string invoiceno)
        {
            return await _repoContext.GetInvoiceHeaderPorId(invoiceno);  
        }
        [HttpGet("GetListDetails")]
        public async Task<List<InvoiceDetailsDTO>> ObtenerListaDetails(string invoceno)
        {
            return await _repoContext.GetInvoiceDetails(invoceno);
            
        }
        [HttpPost("SaveInvoice")]
        public async Task<InvoiceRespuestaDTO> GuardarInvoice([FromBody] InvoiceInput invoicepri)
        {   
            return await _repoContext.Save(invoicepri);
        }
        [HttpDelete("DeleteInvoice")]
        public async Task<InvoiceRespuestaDTO> EliminarInvoice(string invoiceno)
        {
            return await _repoContext.Delete(invoiceno);
        }
    }
}
