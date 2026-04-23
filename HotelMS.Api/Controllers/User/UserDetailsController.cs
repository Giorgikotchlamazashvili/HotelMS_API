using HotelMS.Application.DTOs.Request.UserDetailsRequest;
using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.User
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService;

        public UserDetailsController(IUserDetailsService userDetailsService)
        {
            _userDetailsService = userDetailsService;
        }

        [HttpPost("add-userdetails")]
        public async Task<ActionResult> AddUserDetails(AddUserDetails request)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            request.Mail = email;

            var (success, error, data) = await _userDetailsService.AddUserDetailsAsync(request);

            if (!success)
            {
                return BadRequest(error);
            }

            return Ok(data);
        }

        [HttpPatch("change-user-details")]
        public async Task<ActionResult> ChangeUserDetails(UpdateUserDetails request)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            request.Mail = email;

            var (success, error, data) = await _userDetailsService.UpdateUserDetailsAsync(request);

            if (!success)
            {
                return BadRequest(error);
            }

            return Ok(new { Message = "User details updated successfully", Data = data });
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpGet("get-all-user-details")]
        public async Task<ActionResult> GetAll()
        {
            var (success, error, data) = await _userDetailsService.GetAllUserDetailsAsync();

            if (!success)
            {
                return NotFound(error);
            }

            return Ok(new { Users = data, TotalCount = data.Count() });
        }
    }
}