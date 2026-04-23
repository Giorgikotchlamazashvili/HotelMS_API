using HotelMS.Application.DTOs.Response.Invoice;

namespace HotelMS.Application.Interfaces.Services;

public interface IPdfGeneratorService
{
    byte[] GenerateInvoicePdf(InvoiceResponse invoice);
}