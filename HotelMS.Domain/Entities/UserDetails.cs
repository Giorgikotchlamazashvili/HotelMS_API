using HotelMS.Domain.Common;

namespace HotelMS.Domain.Entities;

public class UserDetails : BaseEntity
{
    public int UserId { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public Users User { get; set; }

}
