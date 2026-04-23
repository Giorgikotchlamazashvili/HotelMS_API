using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public int BookingId { get; set; }
        public List<Bookings> Booking { get; set; }
    }
}
