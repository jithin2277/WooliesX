using System;
using System.Threading.Tasks;
using WooliesX.Data.Entities.Trolley;
using WooliesX.Http;

namespace WooliesX.Data
{
    public interface ITrolleyTotalProcessor : IDisposable
    {
        Task<string> GetTrolleyTotal(TrolleyEntity trolleyEntity);
    }

    public class TrolleyTotalProcessor : ITrolleyTotalProcessor
    {
        private IHttpClientHelper _httpClientHelper;

        public TrolleyTotalProcessor(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper ?? throw new ArgumentNullException(nameof(httpClientHelper));
        }

        public async Task<string> GetTrolleyTotal(TrolleyEntity trolleyEntity)
        {
            return await _httpClientHelper.PostAsync<TrolleyEntity, string>(Constants.TROLLEY_CALCULATOR, trolleyEntity).ConfigureAwait(false);
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

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
