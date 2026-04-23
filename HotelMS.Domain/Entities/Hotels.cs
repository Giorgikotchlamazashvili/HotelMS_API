using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Hotels : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public List<Rooms> Rooms { get; set; }
        public List<Reviews> Reviews { get; set; }
    }
}
