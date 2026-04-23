using AutoMapper;
using HotelMS.Application.DTOs.Request.Hotel;
using HotelMS.Application.DTOs.Request.Rooms;
using HotelMS.Application.DTOs.Request.UserDetailsRequest;
using HotelMS.Application.DTOs.Response.Booking;
using HotelMS.Application.DTOs.Response.HotelResponse;
using HotelMS.Application.DTOs.Response.Invoice;
using HotelMS.Application.DTOs.Response.Payment;
using HotelMS.Application.DTOs.Response.ReviewResponse;
using HotelMS.Application.DTOs.Response.Rooms;
using HotelMS.Application.DTOs.Response.UserDetails;
using HotelMS.Application.DTOs.Response.UserResponse;
using HotelMS.Application.DTOs.UserRequest;
using HotelMS.Domain.Entities;

namespace HotelMS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateUser, Users>().ReverseMap();

            CreateMap<Users, UserDto>();

            CreateMap<UserDetails, AddUserDetails>().ReverseMap();

            CreateMap<UpdateUserDetails, UserDetails>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<UserDetails, UserDetailsDto>()
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<AddCountryRequest, Countries>().ReverseMap();
            CreateMap<AddHotelRequest, Hotels>().ReverseMap();

            CreateMap<Hotels, AddHotelResponse>().ReverseMap();

            CreateMap<AddCityRequest, City>().ReverseMap();

            CreateMap<Reviews, GetReviewResponse>().ReverseMap();

            CreateMap<Rooms, RoomResponse>()
                    .ForMember(dest => dest.HotelName,
                opt => opt.MapFrom(src => src.Hotel != null ? src.Hotel.Name : string.Empty))
                    .ForMember(dest => dest.RoomType,
                opt => opt.MapFrom(src => src.RoomType))
                    .ForMember(dest => dest.Amenities,
                opt => opt.MapFrom(src => src.Amenities));


            CreateMap<CreateRoomRequest, Rooms>()
                .ForMember(dest => dest.Amenities, opt => opt.Ignore());

            CreateMap<RoomType, RoomTypeResponse>();
            CreateMap<CreateRoomTypeRequest, RoomType>();


            CreateMap<Amenity, AmenityResponse>();
            CreateMap<CreateAmenityRequest, Amenity>();

            CreateMap<AddNewEmployeeRequest, Users>();

            CreateMap<Hotels, HotelResponse>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.RoomCount, opt => opt.MapFrom(src => src.Rooms.Count));
            CreateMap<Invoice, InvoiceResponse>()
                .ForMember(dest => dest.Booking, opt => opt.MapFrom(src => src.Booking))
                .ForMember(dest => dest.InvoicePdfUrl, opt => opt.Ignore());


            CreateMap<Bookings, BookingResponse>()
                        .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Name))
                        .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                        .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room.Id))
                        .ForMember(dest => dest.RoomPricePerNight, opt => opt.MapFrom(src => src.Room.PricePerNight));


            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PaidAt, opt => opt.MapFrom(src => src.PaidAt))
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId));
        }
    }
}
