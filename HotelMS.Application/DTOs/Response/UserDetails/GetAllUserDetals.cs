namespace HotelMS.Application.DTOs.Response.UserDetails
{
    public class GetAllUserDetals

    {
        public List<UserDetailsDto> Users { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
