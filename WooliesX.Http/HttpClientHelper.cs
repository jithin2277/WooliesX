using System;
using System.Threading.Tasks;
using System.Net.Http;
using WooliesX.Http.Serialization;

namespace WooliesX.Http
{
    public interface IHttpClientHelper
    {
        Task<T> GetAsync<T>(string url) where T : class;
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
    }
}
