using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities
{
    public class Notifications : BaseEntity
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
