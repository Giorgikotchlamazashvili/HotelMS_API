namespace HotelMS.Application.DTOs.Response.HotelResponse;

public class AddHotelResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
    public int CityId { get; set; }
}
