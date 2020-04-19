using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Api.Model;
using WooliesX.Api.Model.Trolley;
using WooliesX.Data;
using WooliesX.Data.Entities.Trolley;
using WooliesX.Data.Enums;

namespace WooliesX.Api.Controllers
{
    [Route("api/excercise/[controller]")]
    [ApiController]
    public class Excercise3Controller : ControllerBase
    {
        private ITrolleyTotalProcessor _trolleyTotalProcessor;
        private IMapper _mapper;

        public Excercise3Controller(ITrolleyTotalProcessor trolleyTotalProcessor, IMapper mapper)
        {
            _trolleyTotalProcessor = trolleyTotalProcessor ?? throw new ArgumentNullException(nameof(trolleyTotalProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("trolleyTotal")]
        public async Task<ActionResult<string>> GetTrolleyTotal(Trolley trolley)
        {
            var trolleyEntity = _mapper.Map<TrolleyEntity>(trolley);
            return await _trolleyTotalProcessor.GetTrolleyTotal(trolleyEntity).ConfigureAwait(false);
        }
    }
}
