using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.Api.Model;
using WooliesX.Data.Entities;

namespace WooliesX.Api
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserEntity, User>();
            CreateMap<ProductEntity, Product>();
        }
    }
}
