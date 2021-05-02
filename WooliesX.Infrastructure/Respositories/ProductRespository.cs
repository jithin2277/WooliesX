using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities;
using WooliesX.Infrastructure.Http;

namespace WooliesX.Infrastructure.Respositories
{
    public class ProductRespository : IProductRespository
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly string _productsUrl;
        private readonly ILogger<ProductRespository> _logger;

        public ProductRespository(IHttpClientHelper httpClientHelper,
            IOptions<AppSettings> appSettingsOptions,
            ILogger<ProductRespository> logger)
        {
            _httpClientHelper = httpClientHelper;

            var appSettings = appSettingsOptions.Value;
            if (string.IsNullOrEmpty(appSettings.BaseUrl))
            {
                throw new ArgumentNullException("BaseUrl is null or empty");
            }
            if (string.IsNullOrEmpty(appSettings.ProductsEndPoint))
            {
                throw new ArgumentNullException("ProductsEndPoint is null or empty");
            }
            if (string.IsNullOrEmpty(appSettings.Token))
            {
                throw new ArgumentNullException("Token is null or empty");
            }

            _productsUrl = HttpHelper.GenerateUrl(appSettings.BaseUrl, appSettings.ProductsEndPoint, appSettings.Token);
            _logger = logger;
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            _logger.LogInformation($"GetProducts: Getting products from {_productsUrl}");

            return await _httpClientHelper
                .GetAsync<IEnumerable<ProductEntity>>(_productsUrl)
                .ConfigureAwait(false);
        }
    }
}
