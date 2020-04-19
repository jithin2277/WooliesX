using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Api.Model;
using WooliesX.Data.Entities;
using WooliesX.Data;
using AutoMapper;

namespace WooliesX.Api.Controllers
{
    [Route("api/excercise/[controller]")]
    [ApiController]
    public class Excercise1Controller : ControllerBase
    {
        private IUserTokenProcessor _userTokenProcessor;
        private IMapper _mapper;

        public Excercise1Controller(IUserTokenProcessor userTokenProcessor, IMapper mapper)
        {
            _userTokenProcessor = userTokenProcessor ?? throw new ArgumentNullException(nameof(userTokenProcessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("user")]
        public ActionResult<User> GetUser()
        {
            var userEntity = _userTokenProcessor.GetUserToken();
            return Ok(_mapper.Map<User>(userEntity));
        }
    }
}
