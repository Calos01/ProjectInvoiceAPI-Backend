using AutoMapper;
using ProjectInvoiceAPI_Backend.DTO;
using ProjectInvoiceAPI_Backend.Models;

namespace ProjectInvoiceAPI_Backend.Profiles
{
    public class InvoiceProfile:Profile
    {
        public InvoiceProfile()
        {
            CreateMap<TblCustomer, CustomerDTO>().ForMember(data=>data.Status,data=>data.MapFrom(s=>s.IsActive==true?"Activo":"No Activo"));
            CreateMap<CustomerDTO, TblCustomer>();
            CreateMap<TblSalesHeader, InvoiceHeaderDTO>().ReverseMap();
            CreateMap<TblSalesProductInfo, InvoiceDetailsDTO>().ReverseMap();
        }
    }
}
