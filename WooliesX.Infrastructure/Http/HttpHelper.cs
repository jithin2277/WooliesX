namespace WooliesX.Infrastructure.Http
{
    public static class HttpHelper
    {
        public static string GenerateUrl(string baseUrl, string endpoint, string token)
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
