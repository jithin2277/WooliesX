using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Api.Model;
using WooliesX.Data;
using WooliesX.Data.Enums;

namespace WooliesX.Api.Controllers
{
    [Route("api/excercise/[controller]")]
    [ApiController]
    public class Excercise2Controller : ControllerBase
    {
        private IProductsProcessor _productsProcessor;
        private IMapper _mapper;

        public Excercise2Controller(IProductsProcessor productsProcessor, IMapper mapper)
        {
            _productsProcessor = productsProcessor ?? throw new ArgumentNullException(nameof(productsProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productsProcessor.GetProducts();
            return Ok(_mapper.Map<IEnumerable<Product>>(products));
        }

        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Product>>> GetSorted(SortOption? sortOption)
        {
            if (sortOption == null)
            {
                return BadRequest("sortOption is null");
            }

            var products = await _productsProcessor.GetProducts(sortOption.Value);
            return Ok(_mapper.Map<IEnumerable<Product>>(products));
        }
    }
}