using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Countries Country { get; set; }
        public List<Hotels> Hotels { get; set; }
    }
}
