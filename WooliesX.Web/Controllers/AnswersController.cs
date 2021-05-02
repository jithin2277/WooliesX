using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WooliesX.Application.UserDetails;

namespace WooliesX.Web.Controllers
{
    public class AnswersController : ApiControllerBase
    {
        private readonly ILogger<AnswersController> _logger;

        public AnswersController(ILogger<AnswersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> Get()
        {
            _logger.LogInformation("Get: User details");

            return Ok(await Mediator.Send(new GetUserDetailsRequest { Name = "Jithin Jayasankar" }));
        }
    }
}
