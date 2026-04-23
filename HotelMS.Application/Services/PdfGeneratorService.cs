using HotelMS.Application.DTOs.Response.Invoice;
using HotelMS.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

namespace HotelMS.Application.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {

        public byte[] GenerateInvoicePdf(InvoiceResponse invoice)
        {
            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content().Column(col =>
                    {
                        col.Item().Text("INVOICE").FontSize(24).Bold().AlignCenter();
                        col.Item().Text($"Invoice Number: {invoice.InvoiceNumber}");
                        col.Item().Text($"Issued At: {invoice.IssuedAt:yyyy-MM-dd HH:mm:ss}");
                        col.Item().PaddingTop(10).Text(" ");

                        if (invoice.Booking != null && invoice.Booking.Any())
                        {
                            col.Item().Text("Booking Details").Bold();
                            col.Item().Text(" ");
                            foreach (var booking in invoice.Booking)
                            {
                                col.Item().Text($"Booking ID: {booking.BookingId}");
                                col.Item().Text($"Check-In: {booking.CheckInDate:yyyy-MM-dd}");
                                col.Item().Text($"Check-Out: {booking.CheckOutDate:yyyy-MM-dd}");
                                col.Item().Text($"Status: {booking.Status}");
                                col.Item().Text($"Total Price: ${booking.TotalPrice:F2}");
                                col.Item().Text("─────────────────────────────────");
                            }
                            var grandTotal = invoice.Booking.Sum(b => b.TotalPrice);
                            col.Item().Text($"Grand Total: ${grandTotal:F2}").Bold();
                        }
                        col.Item().PaddingTop(20).Text("Thank you for your business!").AlignCenter();
                    });
                });
            }).GeneratePdf();
            return pdfBytes;
        }
    }
}