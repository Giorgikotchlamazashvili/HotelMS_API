namespace HotelMS.Application.DTOs.Request.Booking
{
    public class CreateBookRequest
    {

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int paymentMethodId { get; set; }
        public int RoomId { get; set; }
    }
}
