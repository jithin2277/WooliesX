using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Http;

namespace WooliesX.Data.Repositories
{
    public class RestRespository<T> : IRepository<T> where T : class
    {
        private IHttpClientHelper _httpClientHelper;
        private readonly string _apiUrl;

        public RestRespository(string apiUrl, IHttpClientHelper httpClientHelper)
        {
            if (string.IsNullOrEmpty(apiUrl))
            {
                throw new ArgumentNullException(nameof(apiUrl));
            }
            if (!Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute))
            {
                throw new UriFormatException("Malformed Url");
            }

            _apiUrl = apiUrl;
            _httpClientHelper = httpClientHelper ?? throw new ArgumentNullException(nameof(httpClientHelper));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _httpClientHelper.GetAsync<IEnumerable<T>>(_apiUrl).ConfigureAwait(false);
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
