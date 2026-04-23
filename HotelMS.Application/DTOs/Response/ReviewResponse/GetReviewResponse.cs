namespace HotelMS.Application.DTOs.Response.ReviewResponse
{
    public class GetReviewResponse
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Comment { get; set; }
    }
}
