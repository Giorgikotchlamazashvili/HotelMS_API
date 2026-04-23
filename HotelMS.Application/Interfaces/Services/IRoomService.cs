
using HotelMS.Application.DTOs.Request.Rooms;
using HotelMS.Application.DTOs.Response.Rooms;

namespace HotelMS.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<List<RoomResponse>> GetAllRoomsAsync();
        Task<RoomResponse> GetRoomByIdAsync(int id);
        Task<List<RoomResponse>> GetRoomsByHotelIdAsync(int hotelId);
        Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request);
        Task<RoomResponse> UpdateRoomAsync(int id, UpdateRoomRequest request);
        Task DeleteRoomAsync(int id);

        Task<List<RoomTypeResponse>> GetAllRoomTypesAsync();
        Task<RoomTypeResponse> GetRoomTypeByIdAsync(int id);
        Task<RoomTypeResponse> CreateRoomTypeAsync(CreateRoomTypeRequest request);
        Task<RoomTypeResponse> UpdateRoomTypeAsync(int id, UpdateRoomTypeRequest request);
        Task DeleteRoomTypeAsync(int id);

        Task<List<AmenityResponse>> GetAllAmenitiesAsync();
        Task<AmenityResponse> GetAmenityByIdAsync(int id);
        Task<AmenityResponse> CreateAmenityAsync(CreateAmenityRequest request);
        Task<AmenityResponse> UpdateAmenityAsync(int id, UpdateAmenityRequest request);
        Task DeleteAmenityAsync(int id);
        Task AddAmenityToRoomAsync(int roomId, int amenityId);
        Task RemoveAmenityFromRoomAsync(int roomId, int amenityId);
    }
}