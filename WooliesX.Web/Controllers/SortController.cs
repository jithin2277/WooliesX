using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Application.Products;
using WooliesX.Domain.Enums;

namespace WooliesX.Web.Controllers
{
    public class SortController : ApiControllerBase
    {
        private readonly ILogger<SortController> _logger;

        public SortController(ILogger<SortController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] SortOption sortOption)
        {
            _logger.LogInformation($"GetSorted: Get sorted products by {sortOption}");

            return Ok(await Mediator.Send(new GetProductsRequest
            {
                SortOption = sortOption
            }));
        }
    }
}
