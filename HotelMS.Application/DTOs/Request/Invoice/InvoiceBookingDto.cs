namespace HotelMS.Application.DTOs.Request.Invoice
{
    public class InvoiceBookingDto
    {
        public int BookingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
