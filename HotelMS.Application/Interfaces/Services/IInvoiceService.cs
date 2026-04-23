using HotelMS.Application.DTOs.Response.Invoice;

namespace HotelMS.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetInvoice();
    }
}
