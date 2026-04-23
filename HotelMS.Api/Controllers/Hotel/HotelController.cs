using HotelMS.Application.DTOs.Request.Hotel;
using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.Hotel;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }


    [HttpPost("add-country")]
    public async Task<IActionResult> AddCountry([FromBody] AddCountryRequest request)
    {
        var (success, error, id) = await _hotelService.AddCountryAsync(request);

        if (!success)
        {
            return BadRequest(error);
        }

        return Ok(new { message = "ქვეყანა წარმატებით დაემატა", id });
    }

    [HttpPost("add-city")]
    public async Task<IActionResult> AddCity([FromBody] AddCityRequest request)
    {
        var (success, error, id) = await _hotelService.AddCityAsync(request);

        if (!success)
        {
            return NotFound(error);
        }

        return Ok(new { message = "ქალაქი წარმატებით დაემატა", id });
    }

    [HttpPost("add-hotel")]
    public async Task<IActionResult> AddHotel([FromBody] AddHotelRequest request)
    {
        var (success, error, response) = await _hotelService.AddHotelAsync(request);

        if (!success)
        {
            return BadRequest(error);
        }

        return Ok(new { message = "სასტუმრო წარმატებით დაემატა", data = response });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var (success, error) = await _hotelService.DeleteHotelAsync(id);

        if (!success)
        {
            return NotFound(new { message = error });
        }

        return Ok(new { message = "სასტუმრო წარმატებით წაიშალა" });
    }

    [HttpGet("get-all-hotel")]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await _hotelService.GetAllHotelsAsync();

        if (hotels == null || !hotels.Any())
        {
            return NotFound("სასტუმროები ვერ მოიძებნა.");
        }

        return Ok(hotels);
    }
}