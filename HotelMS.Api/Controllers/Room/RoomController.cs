using HotelMS.Application.DTOs.Request.Rooms;
using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("get-all-rooms")]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("get-room-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var room = await _roomService.GetRoomByIdAsync(id);
                return Ok(room);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("get-rooms-by-hotel-id")]
        public async Task<IActionResult> GetByHotel(int hotelId)
        {
            var rooms = await _roomService.GetRoomsByHotelIdAsync(hotelId);
            return Ok(rooms);
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPost("create-room")]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            try
            {
                var created = await _roomService.CreateRoomAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPut("update-room")]
        public async Task<IActionResult> Update(int id, UpdateRoomRequest request)
        {
            try
            {
                var updated = await _roomService.UpdateRoomAsync(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpDelete("delete-room")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("get-all-room-types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _roomService.GetAllRoomTypesAsync();
            return Ok(types);
        }

        [HttpGet("get-room-type-by-id")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            try
            {
                var roomType = await _roomService.GetRoomTypeByIdAsync(id);
                return Ok(roomType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPost("create-room-type")]
        public async Task<IActionResult> CreateType(CreateRoomTypeRequest request)
        {
            var created = await _roomService.CreateRoomTypeAsync(request);
            return CreatedAtAction(nameof(GetTypeById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPut("update-room-type")]
        public async Task<IActionResult> UpdateType(int id, UpdateRoomTypeRequest request)
        {
            try
            {
                var updated = await _roomService.UpdateRoomTypeAsync(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpDelete("delete-room-type")]
        public async Task<IActionResult> DeleteType(int id)
        {
            try
            {
                await _roomService.DeleteRoomTypeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("get-all-amenities")]
        public async Task<IActionResult> GetAllAmenities()
        {
            var amenities = await _roomService.GetAllAmenitiesAsync();
            return Ok(amenities);
        }

        [HttpGet("get-amenity-by-id")]
        public async Task<IActionResult> GetAmenityById(int id)
        {
            try
            {
                var amenity = await _roomService.GetAmenityByIdAsync(id);
                return Ok(amenity);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPost("create-amenity")]
        public async Task<IActionResult> CreateAmenity(CreateAmenityRequest request)
        {
            var created = await _roomService.CreateAmenityAsync(request);
            return CreatedAtAction(nameof(GetAmenityById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPut("update-amenity")]
        public async Task<IActionResult> UpdateAmenity(int id, UpdateAmenityRequest request)
        {
            try
            {
                var updated = await _roomService.UpdateAmenityAsync(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpDelete("delete-amenity")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            try
            {
                await _roomService.DeleteAmenityAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpPost("add-amenity-to-room")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            try
            {
                await _roomService.AddAmenityToRoomAsync(roomId, amenityId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpDelete("remove-amenity-from-room")]
        public async Task<IActionResult> RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            try
            {
                await _roomService.RemoveAmenityFromRoomAsync(roomId, amenityId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}