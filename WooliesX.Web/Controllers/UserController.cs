using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WooliesX.Application.UserDetails;

namespace WooliesX.Web.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> Get()
        {
            _logger.LogInformation("Get: User details");

            return Ok(
                await Mediator.Send(
                    new GetUserDetailsRequest
                    {
                        Name = "Jithin Jayasankar"
                    }));
        }
    }
}
