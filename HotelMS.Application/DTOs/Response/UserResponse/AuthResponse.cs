namespace HotelMS.Application.DTOs.Response.UserResponse
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
