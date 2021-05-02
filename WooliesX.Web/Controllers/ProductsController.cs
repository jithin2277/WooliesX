using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Application.Products;
using WooliesX.Domain.Enums;

namespace WooliesX.Web.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            _logger.LogInformation($"Get: Get all products");

            return Ok(await Mediator.Send(new GetProductsRequest
            {
                SortOption = SortOption.Low
            }));
        }


        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] SortOption? sortOption)
        {
            _logger.LogInformation($"Get: Get sorted products by {sortOption}");

            return Ok(await Mediator.Send(new GetProductsRequest
            {
                SortOption = sortOption
            }));
        }
    }
}
