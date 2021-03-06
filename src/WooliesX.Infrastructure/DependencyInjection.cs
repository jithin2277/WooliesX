﻿using Microsoft.Extensions.DependencyInjection;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Infrastructure.Respositories;
using WooliesX.Infrastructure.Serialization;

namespace WooliesX.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISerializer, JsonNetSerializer>();

            services.AddScoped<IHttpClientHelper, HttpClientHelper>();

            services.AddScoped<IUserRespository, UserRespository>();
            services.AddScoped<IProductRespository, ProductRespository>();
            services.AddScoped<IShopperRespository, ShopperRespository>();
            services.AddScoped<ITrolleyRepository, TrolleyRepository>();

            return services;
        }
    }
}
