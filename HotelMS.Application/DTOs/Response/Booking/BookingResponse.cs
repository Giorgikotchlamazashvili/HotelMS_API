namespace HotelMS.Application.DTOs.Response.Booking
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string? InvoicePdfUrl { get; set; }

        public int RoomId { get; set; }

        public string UserFullName { get; set; }
        public string UserEmail { get; set; }

        public decimal RoomPricePerNight { get; set; }
    }
}
