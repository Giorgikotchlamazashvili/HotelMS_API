using HotelMS.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.UserRole
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserRoles()
        {
            var roles = Enum.GetValues<UserRoles>()
                .Select(r => new
                {
                    id = (int)r,
                    name = r.ToString()
                });

            return Ok(roles);
        }
    }
}
