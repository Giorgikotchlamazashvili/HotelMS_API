using AutoMapper;
using HotelMS.Application.DTOs.Request.Rooms;
using HotelMS.Application.DTOs.Response.Rooms;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
namespace HotelMS.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }



        public async Task<List<RoomResponse>> GetAllRoomsAsync()
        {
            List<Rooms> rooms = await _roomRepository.GetAllRoomsAsync();
            List<RoomResponse> mapped = _mapper.Map<List<RoomResponse>>(rooms);
            return mapped;
        }

        public async Task<RoomResponse> GetRoomByIdAsync(int id)
        {

            Rooms room = await _roomRepository.GetRoomByIdAsync(id);
            RoomResponse mapped = _mapper.Map<RoomResponse>(room);
            return mapped;
        }

        public async Task<List<RoomResponse>> GetRoomsByHotelIdAsync(int hotelId)
        {
            List<Rooms> rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            List<RoomResponse> mapped = _mapper.Map<List<RoomResponse>>(rooms);
            return mapped;
        }


        public async Task<RoomResponse> CreateRoomAsync(CreateRoomRequest request)
        {
            Rooms room = _mapper.Map<Rooms>(request);
            room.Amenities = new List<Amenity>();

            foreach (int amenityId in request.AmenityIds)
            {

                Amenity amenity = await _roomRepository.GetAmenityByIdAsync(amenityId);
                room.Amenities.Add(amenity);
            }

            Rooms created = await _roomRepository.CreateRoomAsync(room);
            RoomResponse mapped = _mapper.Map<RoomResponse>(created);
            return mapped;
        }

        public async Task<RoomResponse> UpdateRoomAsync(int id, UpdateRoomRequest request)
        {
            Rooms room = await _roomRepository.GetRoomByIdAsync(id);
            room.PricePerNight = request.PricePerNight;
            room.Capacity = request.Capacity;
            room.IsAvailable = request.IsAvailable;
            room.RoomTypeId = request.RoomTypeId;
            room.Amenities = new List<Amenity>();

            foreach (int amenityId in request.AmenityIds)
            {
                Amenity amenity = await _roomRepository.GetAmenityByIdAsync(amenityId);
                room.Amenities.Add(amenity);
            }

            Rooms updated = await _roomRepository.UpdateRoomAsync(room);
            RoomResponse mapped = _mapper.Map<RoomResponse>(updated);
            return mapped;
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteRoomAsync(id);
        }


        public async Task<List<RoomTypeResponse>> GetAllRoomTypesAsync()
        {
            List<RoomType> types = await _roomRepository.GetAllRoomTypesAsync();
            List<RoomTypeResponse> mapped = _mapper.Map<List<RoomTypeResponse>>(types);
            return mapped;
        }

        public async Task<RoomTypeResponse> GetRoomTypeByIdAsync(int id)
        {
            RoomType roomType = await _roomRepository.GetRoomTypeByIdAsync(id);
            RoomTypeResponse mapped = _mapper.Map<RoomTypeResponse>(roomType);
            return mapped;
        }

        public async Task<RoomTypeResponse> CreateRoomTypeAsync(CreateRoomTypeRequest request)
        {
            RoomType roomType = _mapper.Map<RoomType>(request);
            RoomType created = await _roomRepository.CreateRoomTypeAsync(roomType);
            RoomTypeResponse mapped = _mapper.Map<RoomTypeResponse>(created);
            return mapped;
        }

        public async Task<RoomTypeResponse> UpdateRoomTypeAsync(int id, UpdateRoomTypeRequest request)
        {
            RoomType roomType = await _roomRepository.GetRoomTypeByIdAsync(id);
            roomType.Name = request.Name;
            roomType.Description = request.Description;

            RoomType updated = await _roomRepository.UpdateRoomTypeAsync(roomType);
            RoomTypeResponse mapped = _mapper.Map<RoomTypeResponse>(updated);
            return mapped;
        }

        public async Task DeleteRoomTypeAsync(int id)
        {
            await _roomRepository.DeleteRoomTypeAsync(id);
        }

        public async Task<List<AmenityResponse>> GetAllAmenitiesAsync()
        {
            List<Amenity> amenities = await _roomRepository.GetAllAmenitiesAsync();
            List<AmenityResponse> mapped = _mapper.Map<List<AmenityResponse>>(amenities);
            return mapped;
        }

        public async Task<AmenityResponse> GetAmenityByIdAsync(int id)
        {
            Amenity amenity = await _roomRepository.GetAmenityByIdAsync(id);
            AmenityResponse mapped = _mapper.Map<AmenityResponse>(amenity);
            return mapped;
        }

        public async Task<AmenityResponse> CreateAmenityAsync(CreateAmenityRequest request)
        {
            Amenity amenity = _mapper.Map<Amenity>(request);
            Amenity created = await _roomRepository.CreateAmenityAsync(amenity);
            AmenityResponse mapped = _mapper.Map<AmenityResponse>(created);
            return mapped;
        }

        public async Task<AmenityResponse> UpdateAmenityAsync(int id, UpdateAmenityRequest request)
        {
            Amenity amenity = await _roomRepository.GetAmenityByIdAsync(id);
            amenity.Name = request.Name;

            Amenity updated = await _roomRepository.UpdateAmenityAsync(amenity);
            AmenityResponse mapped = _mapper.Map<AmenityResponse>(updated);
            return mapped;
        }

        public async Task DeleteAmenityAsync(int id)
        {
            await _roomRepository.DeleteAmenityAsync(id);
        }

        public async Task AddAmenityToRoomAsync(int roomId, int amenityId)
        {
            await _roomRepository.AddAmenityToRoomAsync(roomId, amenityId);
        }

        public async Task RemoveAmenityFromRoomAsync(int roomId, int amenityId)
        {
            await _roomRepository.RemoveAmenityFromRoomAsync(roomId, amenityId);
        }
    }
}