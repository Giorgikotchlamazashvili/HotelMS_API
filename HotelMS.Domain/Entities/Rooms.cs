using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Rooms : BaseEntity
    {
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }

        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public List<Amenity> Amenities { get; set; }
        public List<Bookings> Bookings { get; set; }
    }
}
