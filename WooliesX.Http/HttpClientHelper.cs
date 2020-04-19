using System;
using System.Threading.Tasks;
using System.Net.Http;
using WooliesX.Http.Serialization;
using System.Text;

namespace WooliesX.Http
{
    public interface IHttpClientHelper
    {
        Task<T> GetAsync<T>(string url) where T : class;
        Task<TResponse> PostAsync<TRequestBody, TResponse>(string uri, TRequestBody data) where TRequestBody : class where TResponse : class;
    }

    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly ISerializer _serializer;

        public HttpClientHelper() : this(new JsonSerializer())
        { }

        public HttpClientHelper(ISerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task<T> GetAsync<T>(string url) where T : class
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return _serializer.Deserialize<T>(jsonString);
                }

                return null;
            }
        }

        public async Task<TResponse> PostAsync<TRequestBody, TResponse>(string uri, TRequestBody data) where TRequestBody : class where TResponse : class
        {
            var content = new StringContent(_serializer.Serialize(data));
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(uri, content).ConfigureAwait(false);
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return _serializer.Deserialize<TResponse>(jsonString);
                }

                return null;
            }
        }
    }
}
