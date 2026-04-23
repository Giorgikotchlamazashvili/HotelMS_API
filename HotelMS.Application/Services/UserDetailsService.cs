using AutoMapper;
using HotelMS.Application.DTOs.Request.UserDetailsRequest;
using HotelMS.Application.DTOs.Response.UserDetails;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;

namespace HotelMS.Application.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;

        public UserDetailsService(IUserDetailsRepository userDetailsRepository, IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }

        public async Task<(bool success, string error, UserDetailsDto data)> AddUserDetailsAsync(AddUserDetails request)
        {
            var user = await _userDetailsRepository.GetUserWithDetailsByEmailAsync(request.Mail);

            if (user == null)
            {
                return (false, "მომხმარებელი ვერ მოიძებნა.", null);
            }

            if (!user.IsValidated)
            {
                return (false, "მომხმარებელი არ არის ვერიფიცირებული.", null);
            }

            if (user.UserDetails != null)
            {
                return (false, "მომხმარებლის დეტალები უკვე არსებობს.", null);
            }

            var userDetails = _mapper.Map<UserDetails>(request);
            user.UserDetails = userDetails;

            await _userDetailsRepository.SaveChangesAsync();

            return (true, null, _mapper.Map<UserDetailsDto>(userDetails));
        }

        public async Task<(bool success, string error, UserDetailsDto data)> UpdateUserDetailsAsync(UpdateUserDetails request)
        {
            var user = await _userDetailsRepository.GetUserWithDetailsByEmailAsync(request.Mail);

            if (user == null)
            {
                return (false, "მომხმარებელი ვერ მოიძებნა.", null);
            }

            if (user.UserDetails == null)
            {
                return (false, "მომხმარებლის დეტალები არ არსებობს. გამოიყენეთ დამატება.", null);
            }

            _mapper.Map(request, user.UserDetails);

            await _userDetailsRepository.SaveChangesAsync();

            return (true, null, _mapper.Map<UserDetailsDto>(user.UserDetails));
        }

        public async Task<(bool success, string error, IEnumerable<UserDetailsDto> data)> GetAllUserDetailsAsync()
        {
            var detailsList = await _userDetailsRepository.GetAllUserDetailsAsync();

            if (detailsList == null || !detailsList.Any())
            {
                return (false, "მონაცემები ვერ მოიძებნა.", null);
            }

            return (true, null, _mapper.Map<IEnumerable<UserDetailsDto>>(detailsList));
        }
    }
}