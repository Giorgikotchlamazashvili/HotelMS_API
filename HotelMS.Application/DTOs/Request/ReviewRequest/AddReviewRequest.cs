namespace HotelMS.Application.DTOs.Request.ReviewRequest
{
    public class AddReviewRequest
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public int HotelId { get; set; }

    }
}
