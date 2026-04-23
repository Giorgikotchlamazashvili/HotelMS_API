namespace HotelMS.Application.DTOs.Response.HotelResponse
{
    public class HotelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public string CityName { get; set; }
        public int RoomCount { get; set; }
    }
}
