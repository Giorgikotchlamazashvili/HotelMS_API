using HotelMS.Application.DTOs.Request.Invoice;

namespace HotelMS.Application.DTOs.Response.Invoice
{
    public class InvoiceResponse
    {
        public string InvoiceNumber { get; set; }
        public DateTime IssuedAt { get; set; }
        public string InvoicePdfUrl { get; set; }
        public List<InvoiceBookingDto> Booking { get; set; }
    }
}