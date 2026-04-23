namespace HotelMS.Application.DTOs.Request.Rooms
{
    public class CreateRoomRequest
    {
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public List<int> AmenityIds { get; set; }
    }
}
