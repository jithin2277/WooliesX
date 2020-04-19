using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesX.Http.Serialization
{
    public interface ISerializer
    {
        string Serialize(object data);
        T Deserialize<T>(string value);
    }
}
