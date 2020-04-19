using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.Api.Model;
using WooliesX.Api.Model.Trolley;
using WooliesX.Data.Entities;
using WooliesX.Data.Entities.Trolley;

namespace WooliesX.Api
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserEntity, User>();
            CreateMap<ProductEntity, Product>();
            
            CreateMap<TrolleyEntity, Trolley>();
            CreateMap<TrolleyProductEntity, TrolleyProduct>();
            CreateMap<TrolleyProductQuantityEntity, TrolleyProductQuantity>();
            CreateMap<TrolleySpecialEntity, TrolleySpecial>();
        }
    }
}
