using WooliesX.Infrastructure.Http;
using Xunit;

namespace WooliesX.Infrastructure.Tests
{
    public class HttpHelperTests
    {
        [Theory]
        [InlineData("http://www.foo.com/", "/u1", "token")]
        [InlineData("http://www.foo.com", "u1/", "token")]
        public void GenerateUrl_ShouldReturnMergedUrl(string baseUrl, string endpoint, string token)
        {
            var expected = GenerateUrl(baseUrl, endpoint, token);
            var actual = HttpHelper.GenerateUrl(baseUrl, endpoint, token);

            Assert.Equal(expected, actual);
        }

        private string GenerateUrl(string baseUrl, string endpoint, string token)
        {
            if (baseUrl.EndsWith('/'))
            {
                baseUrl = baseUrl.Remove(baseUrl.LastIndexOf('/'));
            }
            if (endpoint.StartsWith('/'))
            {
                endpoint = endpoint.Remove(endpoint.IndexOf('/'));
            }

            return $"{baseUrl}/{endpoint}?token={token}";
        }
    }
}
