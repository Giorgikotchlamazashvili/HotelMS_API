namespace HotelMS.Application.DTOs.Response.UserResponse
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsValidated { get; set; }
    }
}
