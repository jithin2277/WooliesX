using System;
using System.Text.Json;

namespace WooliesX.Infrastructure.Serialization
{
    public class DefaultSerializer : ISerializer
    {
        public T Deserialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public string Serialize(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return JsonSerializer.Serialize(value);
        }
    }
}
