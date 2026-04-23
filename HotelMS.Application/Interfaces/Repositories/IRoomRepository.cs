using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Rooms>> GetAllRoomsAsync();
        Task<Rooms> GetRoomByIdAsync(int id);
        Task<List<Rooms>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Rooms> CreateRoomAsync(Rooms room);
        Task<Rooms> UpdateRoomAsync(Rooms room);
        Task DeleteRoomAsync(int id);
        Task<bool> RoomExistsAsync(int id);

        Task<List<RoomType>> GetAllRoomTypesAsync();
        Task<RoomType> GetRoomTypeByIdAsync(int id);
        Task<RoomType> CreateRoomTypeAsync(RoomType roomType);
        Task<RoomType> UpdateRoomTypeAsync(RoomType roomType);
        Task DeleteRoomTypeAsync(int id);

        Task<List<Amenity>> GetAllAmenitiesAsync();
        Task<Amenity> GetAmenityByIdAsync(int id);
        Task<Amenity> CreateAmenityAsync(Amenity amenity);
        Task<Amenity> UpdateAmenityAsync(Amenity amenity);
        Task DeleteAmenityAsync(int id);
        Task AddAmenityToRoomAsync(int roomId, int amenityId);
        Task RemoveAmenityFromRoomAsync(int roomId, int amenityId);
    }
}