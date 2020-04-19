using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WooliesX.Http.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string value) where T : class
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return JsonConvert.SerializeObject(value);
        }
    }
}
