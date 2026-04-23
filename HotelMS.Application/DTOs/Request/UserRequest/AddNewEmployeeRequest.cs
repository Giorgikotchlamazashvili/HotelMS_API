

using HotelMS.Domain.Enums;

namespace HotelMS.Application.DTOs.UserRequest;

public class AddNewEmployeeRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoles Role { get; set; }

}
