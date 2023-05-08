using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Container
{
    public class RepositoryInvoice : IRepositoryInvoice
    {
        private readonly InvoiceDbContext _context;
        private readonly IMapper _mapper;
        public RepositoryInvoice(InvoiceDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;   
        }

        public async Task<List<InvoiceHeaderDTO>> GetAllInvoiceHeader()
        {
            var listinvoice = await _context.TblSalesHeaders.ToListAsync();
            if (listinvoice != null)
            {
                return  _mapper.Map<List<InvoiceHeaderDTO>>(listinvoice);
            }

            //retorna lista generica de objetos InvoiceHeader
            return new List<InvoiceHeaderDTO>();//Como no recibe parametros es una lista vacia
        }
        public async Task<InvoiceHeaderDTO> GetInvoiceHeaderPorId(string invoiceno)
        {
            var invoiceid = await _context.TblSalesHeaders.FirstOrDefaultAsync(data => data.InvoiceNo == invoiceno);
            if (invoiceid != null)
            {
                return _mapper.Map<InvoiceHeaderDTO>(invoiceid);
            }
            return new InvoiceHeaderDTO();
        }

        public async Task<List<InvoiceDetailsDTO>> GetInvoiceDetails(string invoiceno)
        {
            //where para jalar varios datoscon mismo codigo
            var invoicedetail= await _context.TblSalesProductInfos.Where(data=> data.InvoiceNo==invoiceno).ToListAsync();
            if(invoicedetail != null)
            {
                return _mapper.Map<List<InvoiceDetailsDTO>>(invoicedetail);
            }
            return new List<InvoiceDetailsDTO>();
        }

       
    }
}
