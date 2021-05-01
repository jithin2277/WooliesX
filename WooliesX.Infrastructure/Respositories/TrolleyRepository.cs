using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities.Trolley;
using WooliesX.Infrastructure.Http;

namespace WooliesX.Infrastructure.Respositories
{
    public class TrolleyRepository : ITrolleyRepository
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly string _trolleyTotalUrl;
        private readonly ILogger<TrolleyRepository> _logger;

        public TrolleyRepository(IHttpClientHelper httpClientHelper, IOptions<AppSettings> appSettingsOptions, ILogger<TrolleyRepository> logger)
        {
            _httpClientHelper = httpClientHelper;

            var appSettings = appSettingsOptions.Value;
            if (string.IsNullOrEmpty(appSettings.BaseUrl))
            {
                throw new ArgumentNullException("BaseUrl is null or empty");
            }
            if (string.IsNullOrEmpty(appSettings.TrolleyCalculatorEndPoint))
            {
                throw new ArgumentNullException("TrolleyCalculatorEndPoint is null or empty");
            }

            _trolleyTotalUrl = HttpHelper.GenerateUrl(appSettings.BaseUrl, appSettings.TrolleyCalculatorEndPoint);
            _logger = logger;
        }

        public async Task<string> GetTrolleyTotal(TrolleyEntity trolleyEntity)
        {
            return await _httpClientHelper.PostAsync<TrolleyEntity, string>(_trolleyTotalUrl, trolleyEntity).ConfigureAwait(false);
        }
    }
}
