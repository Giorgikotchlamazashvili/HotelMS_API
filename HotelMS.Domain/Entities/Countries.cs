using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Countries : BaseEntity
    {
        public string Name { get; set; }
        public List<City> Cities { get; set; }
    }
}
