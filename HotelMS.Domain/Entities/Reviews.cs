using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Reviews : BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int HotelId { get; set; }
        public Hotels Hotel { get; set; }
    }
}
