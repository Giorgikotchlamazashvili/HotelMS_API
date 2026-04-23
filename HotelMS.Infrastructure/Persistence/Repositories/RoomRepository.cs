using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly Baza _context;

        public RoomRepository(Baza context)
        {
            _context = context;
        }


        public async Task<List<Rooms>> GetAllRoomsAsync()
        {
            List<Rooms> rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomType)
                .Include(r => r.Amenities)
                .ToListAsync();

            return rooms;
        }

        public async Task<Rooms> GetRoomByIdAsync(int id)
        {
            Rooms room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Amenities)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
            {
                throw new KeyNotFoundException($"Room with id {id} was not found.");
            }

            return room;
        }

        public async Task<List<Rooms>> GetRoomsByHotelIdAsync(int hotelId)
        {
            List<Rooms> rooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Amenities)
                .Where(r => r.HotelId == hotelId)
                .AsNoTracking()
                .ToListAsync();

            return rooms;
        }

        public async Task<Rooms> CreateRoomAsync(Rooms room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return await _context.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Amenities)
                .FirstAsync(r => r.Id == room.Id);
        }
        public async Task<Rooms> UpdateRoomAsync(Rooms room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task DeleteRoomAsync(int id)
        {
            Rooms room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                throw new KeyNotFoundException($"Room with id {id} was not found.");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RoomExistsAsync(int id)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == id);
        }


        public async Task<List<RoomType>> GetAllRoomTypesAsync()
        {
            List<RoomType> types = await _context.RoomTypes.ToListAsync();
            return types;
        }

        public async Task<RoomType> GetRoomTypeByIdAsync(int id)
        {
            RoomType roomType = await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == id);

            if (roomType == null)
            {
                throw new KeyNotFoundException($"RoomType with id {id} was not found.");
            }

            return roomType;
        }

        public async Task<RoomType> CreateRoomTypeAsync(RoomType roomType)
        {
            await _context.RoomTypes.AddAsync(roomType);
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task<RoomType> UpdateRoomTypeAsync(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task DeleteRoomTypeAsync(int id)
        {
            RoomType roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                throw new KeyNotFoundException($"RoomType with id {id} was not found.");
            }

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Amenity>> GetAllAmenitiesAsync()
        {
            List<Amenity> amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenity> GetAmenityByIdAsync(int id)
        {
            Amenity amenity = await _context.Amenities.FirstOrDefaultAsync(a => a.Id == id);

            if (amenity == null)
            {
                throw new KeyNotFoundException($"Amenity with id {id} was not found.");
            }

            return amenity;
        }

        public async Task<Amenity> CreateAmenityAsync(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task<Amenity> UpdateAmenityAsync(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task DeleteAmenityAsync(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
            {
                throw new KeyNotFoundException($"Amenity with id {id} was not found.");
            }

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAmenityToRoomAsync(int roomId, int amenityId)
        {
            Rooms room = await _context.Rooms
                .Include(r => r.Amenities)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                throw new KeyNotFoundException($"Room with id {roomId} was not found.");
            }

            Amenity amenity = await _context.Amenities.FindAsync(amenityId);

            if (amenity == null)
            {
                throw new KeyNotFoundException($"Amenity with id {amenityId} was not found.");
            }

            if (!room.Amenities.Any(a => a.Id == amenityId))
            {
                room.Amenities.Add(amenity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAmenityFromRoomAsync(int roomId, int amenityId)
        {
            Rooms room = await _context.Rooms
                .Include(r => r.Amenities)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
            {
                throw new KeyNotFoundException($"Room with id {roomId} was not found.");
            }

            Amenity amenity = room.Amenities.FirstOrDefault(a => a.Id == amenityId);

            if (amenity == null)
            {
                throw new KeyNotFoundException($"Amenity with id {amenityId} was not found in room {roomId}.");
            }

            room.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();
        }
    }
}
