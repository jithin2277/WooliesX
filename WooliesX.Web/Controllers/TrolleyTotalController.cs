using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WooliesX.Application.TrolleyTotal;

namespace WooliesX.Web.Controllers
{
    public class TrolleyTotalController : ApiControllerBase
    {
        private readonly ILogger<TrolleyTotalController> _logger;

        public TrolleyTotalController(ILogger<TrolleyTotalController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult<string>> Post(TrolleyDto trolley)
        {
            _logger.LogInformation($"GetTrolleyTotal: Get total of Trolley: Request : {JsonConvert.SerializeObject(trolley)}");

            var total = await Mediator.Send(new GetTrolleyTotalRequest
            {
                Trolley = trolley
            });

            _logger.LogInformation($"GetTrolleyTotal: Got total of: {total}");

            return Ok(total);
        }
    }
}
