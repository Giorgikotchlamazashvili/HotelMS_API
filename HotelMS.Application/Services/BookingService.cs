using AutoMapper;
using HotelMS.Application.DTOs.Request.Booking;
using HotelMS.Application.DTOs.Response.Booking;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
using HotelMS.Domain.Events;
using HotelMS.Domain.Helpers;
using HotelMS.Interfaces.Repositories;
using MassTransit;

namespace HotelMS.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public BookingService(
            IBookingRepository repository,
            IPublishEndpoint publishEndpoint,
            IPaymentService paymentService,
            IMapper mapper)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<(bool success, string error, BookingResponse data)> CreateBookingAsync(CreateBookRequest request, int userId)
        {
            var room = await _repository.GetRoomByIdAsync(request.RoomId);

            if (room == null)
            {
                return (false, "ოთახი ვერ მოიძებნა.", null);
            }

            var isAvailable = await _repository.CheckRoomAvailability(
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate);

            if (isAvailable == false)
            {
                return (false, "ოთახი დაკავებულია.", null);
            }

            decimal totalPrice = PriceCalculator.Calculate(
                request.CheckInDate,
                request.CheckOutDate,
                room.PricePerNight);

            var newBooking = new Bookings
            {
                RoomId = request.RoomId,
                UserId = userId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                CreatedAt = DateTime.Now,
                Status = "Pending",
                TotalPrice = totalPrice
            };

            try
            {
                await _repository.AddAsync(newBooking);
                await _repository.SaveChangesAsync();

                var paymentResult = await _paymentService.CreatePaymentAsync(
                    newBooking.Id,
                    totalPrice,
                    request.paymentMethodId);

                await _publishEndpoint.Publish(new BookingConfirmedEvent
                {
                    UserId = userId,
                    BookingId = newBooking.Id
                });

                var createdBooking = await _repository.GetByIdAsync(newBooking.Id);
                var response = _mapper.Map<BookingResponse>(createdBooking);

                if (paymentResult.success == false)
                {
                    return (
                        true,
                        $"ჯავშანი შეიქმნა, მაგრამ გადახდის პრობლემაა: {paymentResult.error}",
                        response
                    );
                }

                return (true, null, response);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);

            if (booking == null)
            {
                throw new KeyNotFoundException($"ჯავშანი ID-ით {id} ვერ მოიძებნა.");
            }

            _repository.Delete(booking);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
        {
            var bookings = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingResponse>>(bookings);
        }

        public async Task<(bool success, string error, BookingResponse data)> GetBookingByIdAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);

            if (booking == null)
            {
                return (false, "ჯავშანი მოცემული ID-ით ვერ მოიძებნა.", null);
            }

            var response = _mapper.Map<BookingResponse>(booking);
            return (true, null, response);
        }

        public async Task<Rooms> GetRoomByIdAsync(int id)
        {
            var room = await _repository.GetRoomByIdAsync(id);

            if (room == null)
            {
                return null;
            }

            return room;
        }

        public async Task<IEnumerable<BookingResponse>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _repository.GetUserBookingsAsync(userId);
            return _mapper.Map<IEnumerable<BookingResponse>>(bookings);
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            bool isAvailable = await _repository.CheckRoomAvailability(roomId, checkIn, checkOut);
            return isAvailable;
        }

        public async Task<(bool success, string error, BookingResponse data)> UpdateBookingAsync(int id, UpdateBookingRequest request)
        {
            var existingBooking = await _repository.GetByIdAsync(id);

            if (existingBooking == null)
            {
                return (false, "ჯავშანი ვერ მოიძებნა.", null);
            }

            if (existingBooking.CheckInDate != request.CheckInDate ||
                existingBooking.CheckOutDate != request.CheckOutDate)
            {
                var isAvailable = await _repository.CheckRoomAvailability(
                    existingBooking.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate);

                if (isAvailable == false)
                {
                    return (false, "არჩეული თარიღები უკვე დაკავებულია.", null);
                }

                var room = await _repository.GetRoomByIdAsync(existingBooking.RoomId);

                existingBooking.TotalPrice = PriceCalculator.Calculate(
                    request.CheckInDate,
                    request.CheckOutDate,
                    room.PricePerNight);
            }

            existingBooking.CheckInDate = request.CheckInDate;
            existingBooking.CheckOutDate = request.CheckOutDate;
            existingBooking.Status = request.Status;

            _repository.Update(existingBooking);
            await _repository.SaveChangesAsync();

            var updatedBooking = await _repository.GetByIdAsync(id);
            var response = _mapper.Map<BookingResponse>(updatedBooking);

            return (true, null, response);
        }
    }
}