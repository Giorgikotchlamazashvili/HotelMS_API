using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Amenity : BaseEntity
    {
        public string Name { get; set; }
        public List<Rooms> Rooms { get; set; }
    }
}
