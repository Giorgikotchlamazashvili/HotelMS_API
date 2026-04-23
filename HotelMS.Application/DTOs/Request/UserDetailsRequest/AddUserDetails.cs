namespace HotelMS.Application.DTOs.Request.UserDetailsRequest
{
    public class AddUserDetails
    {
        public string Mail { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
