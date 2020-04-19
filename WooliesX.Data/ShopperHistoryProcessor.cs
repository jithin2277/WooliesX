using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Data.Entities;
using System.Linq;
using WooliesX.Http;

namespace WooliesX.Data
{
    public interface IShopperHistoryProcessor : IDisposable
    {
        Task<IEnumerable<ShopperHistoryEntity>> GetShopperHistory();
    }

    public class ShopperHistoryProcessor : IShopperHistoryProcessor
    {
        private IHttpClientHelper _httpClientHelper;

        public ShopperHistoryProcessor(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper
                ?? throw new ArgumentNullException(nameof(httpClientHelper));
        }

        public async Task<IEnumerable<ShopperHistoryEntity>> GetShopperHistory()
        {
            return await _httpClientHelper
                .GetAsync<IEnumerable<ShopperHistoryEntity>>(Constants.SHOPPER_HISTORY_API_URL)
                .ConfigureAwait(false);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_httpClientHelper != null)
                    {
                        _httpClientHelper = null;
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
