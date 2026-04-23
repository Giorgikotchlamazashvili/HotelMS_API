namespace HotelMS.Application.DTOs.Response.Rooms
{

    public class RoomResponse
    {
        public int Id { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public RoomTypeResponse RoomType { get; set; }
        public List<AmenityResponse> Amenities { get; set; }
    }
}
