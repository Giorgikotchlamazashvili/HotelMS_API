using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class RoomType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Rooms> Rooms { get; set; }
    }
}
