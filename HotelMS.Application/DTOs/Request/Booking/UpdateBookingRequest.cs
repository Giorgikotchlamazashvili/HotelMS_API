namespace HotelMS.Application.DTOs.Request.Booking
{
    public class UpdateBookingRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
    }
}
