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
    public class ShopperRespository : IShopperRespository
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly string _shopperHistoryUrl;
        private readonly ILogger<ShopperRespository> _logger;

        public ShopperRespository(IHttpClientHelper httpClientHelper, IOptions<AppSettings> appSettingsOptions, ILogger<ShopperRespository> logger)
        {
            _httpClientHelper = httpClientHelper;

            var appSettings = appSettingsOptions.Value;
            if (string.IsNullOrEmpty(appSettings.BaseUrl))
            {
                throw new ArgumentNullException("BaseUrl is null or empty");
            }
            if (string.IsNullOrEmpty(appSettings.ShopperHistoryEndPoint))
            {
                throw new ArgumentNullException("ShopperHistoryEndPoint is null or empty");
            }
            if (string.IsNullOrEmpty(appSettings.Token))
            {
                throw new ArgumentNullException("Token is null or empty");
            }

            _shopperHistoryUrl = HttpHelper.GenerateUrl(appSettings.BaseUrl, appSettings.ShopperHistoryEndPoint, appSettings.Token);
            _logger = logger;
        }

        public async Task<IEnumerable<ShopperHistoryEntity>> GetShopperHistory()
        {
            _logger.LogInformation($"GetShopperHistory: Getting shopper history from {_shopperHistoryUrl}");

            return await _httpClientHelper
                .GetAsync<IEnumerable<ShopperHistoryEntity>>(_shopperHistoryUrl)
                .ConfigureAwait(false);
        }
    }
}
