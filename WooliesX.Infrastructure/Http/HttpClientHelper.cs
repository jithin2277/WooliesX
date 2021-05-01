using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Infrastructure.Serialization;

namespace WooliesX.Infrastructure
{
    public interface IHttpClientHelper
    {
        Task<T> GetAsync<T>(string uri) where T : class;
        Task<TResponse> PostAsync<TRequestBody, TResponse>(string uri, TRequestBody data) where TRequestBody : class where TResponse : class;
    }

    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly ISerializer _serializer;

        public HttpClientHelper(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task<T> GetAsync<T>(string uri) where T : class
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri).ConfigureAwait(false);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return _serializer.Deserialize<T>(jsonString);
            }

            return null;
        }

        public async Task<TResponse> PostAsync<TRequestBody, TResponse>(string uri, TRequestBody data) where TRequestBody : class where TResponse : class
        {
            var content = new StringContent(_serializer.Serialize(data), Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
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
