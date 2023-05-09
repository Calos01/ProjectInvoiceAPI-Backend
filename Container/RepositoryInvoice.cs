using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;
using System.Reflection.PortableExecutable;

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

        public async Task<InvoiceRespuestaDTO> Save(InvoicePrincipaldto invoicepri)
        {
            string Result = string.Empty;
            int process = 0;
            var response = new InvoiceRespuestaDTO();
            if (invoicepri != null)
            {
                using (var con = await this._context.Database.BeginTransactionAsync())
                {
                    if (invoicepri.header != null)
                    {
                        Result = await this.SaveHeader(invoicepri.header);
                        if (!string.IsNullOrEmpty(Result) && (invoicepri.details != null && invoicepri.details.Count > 0))
                        {
                            invoicepri.details.ForEach(item =>
                            {
                                bool saveresult = this.SaveDetail(item).Result;
                                if (saveresult)
                                {
                                    process++;
                                }
                            });
                            if (invoicepri.details.Count == process)
                            {
                                await this._context.SaveChangesAsync();
                                await con.CommitAsync(); //VERIFICA SI SE GUARDO LOS CAMBIOS EN LA BD
                                response.respuesta = "PASO";
                                response.respuesta = Result;
                            }
                        }
                        else
                        {
                            await con.RollbackAsync();//Devuelve el error
                            response.respuesta = "NO PASO";
                            response.respuesta = string.Empty;
                        }
                    }
                };             
            }
            else
            {
                return new InvoiceRespuestaDTO();
            }
            return response;
        }
        private async Task<string> SaveHeader(InvoiceHeaderDTO invoiceheader)
        {
            string Result = string.Empty;
            try
            {
                TblSalesHeader _header = this._mapper.Map<TblSalesHeader>(invoiceheader);
                var headerid = await _context.TblSalesHeaders.FirstOrDefaultAsync(data => data.InvoiceNo == invoiceheader.InvoiceNo);
                if (headerid != null)//si no existe el invoice lo seteara con los datos que vienen
                {
                    headerid.CustomerId = invoiceheader.CustomerId;
                    headerid.CustomerName = invoiceheader.CustomerName;
                    headerid.DeliveryAddress = invoiceheader.DeliveryAddress;
                    headerid.Total = invoiceheader.Total;
                    headerid.Remarks = invoiceheader.Remarks;
                    headerid.Tax = invoiceheader.Tax;
                    headerid.NetTotal = invoiceheader.NetTotal;
                    headerid.ModifyUser = invoiceheader.CreateUser;
                    headerid.ModifyDate = DateTime.Now;

                    //Eliminando la lista de productos con ese codigo invoiceno
                    var _detdata = await this._context.TblSalesProductInfos.Where(data => data.InvoiceNo == invoiceheader.InvoiceNo).ToListAsync();
                    if (_detdata != null)
                    {
                        this._context.TblSalesProductInfos.RemoveRange(_detdata);// TblSalesProductInfos es la lista de ProductInfos, esta jalando de InvoiceDbContext
                    }
                }
                else
                {
                    //si es null entonces agregara el invoice header
                    await this._context.TblSalesHeaders.AddAsync(_header);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        private async Task<bool> SaveDetail(InvoiceDetailsDTO invoicedetail)
        {
            try
            {
                TblSalesProductInfo _datdetail = _mapper.Map<TblSalesProductInfo>(invoicedetail);
                await this._context.TblSalesProductInfos.AddAsync(_datdetail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<InvoiceRespuestaDTO> Delete(string invoiceno)
        {
            try
            {
                //Eliminando el header
                var _header = await _context.TblSalesHeaders.SingleOrDefaultAsync(data => data.InvoiceNo == invoiceno);
                if (_header != null)
                {
                    _context.TblSalesHeaders.Remove(_header);
                }

                //Eliminando la lista de productos con ese codigo invoiceno
                var _detdata = await _context.TblSalesProductInfos.Where(data => data.InvoiceNo == invoiceno).ToListAsync();
                if (_detdata != null)
                {
                    _context.TblSalesProductInfos.RemoveRange(_detdata);
                }
                return new InvoiceRespuestaDTO()
                {
                    respuesta = "Paso",
                    keyvalue = invoiceno
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }       
}
