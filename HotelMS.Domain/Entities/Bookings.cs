using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Bookings : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }

        public string? InvoicePdfUrl { get; set; }

        public int RoomId { get; set; }
        public Rooms Room { get; set; }

        public Payment? Payment { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
