using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectInvoiceAPI_Backend.Container;

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
        public async Task<IActionResult> ObtenerListaInvoice()
        {
            var invoices= await _repoContext.GetAllInvoiceHeader();
            return Ok(invoices);
        }
        [HttpGet("GetInvoiceId")]
        public async Task<IActionResult> ObtenerInvoiceId(string invoiceno)
        {
            var invoiceid = await _repoContext.GetInvoiceHeaderPorId(invoiceno);
            return Ok(invoiceid);   
        }
        [HttpGet("GetListDetails")]
        public async Task<IActionResult> ObtenerListaDetails(string invoceno)
        {
            var details = await _repoContext.GetInvoiceDetails(invoceno);
            return Ok(details);
        }
    }
}
