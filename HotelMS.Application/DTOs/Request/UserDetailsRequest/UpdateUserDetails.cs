namespace HotelMS.Application.DTOs.Request.UserDetailsRequest
{
    public class UpdateUserDetails
    {
        public string Mail { get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
    }
}
