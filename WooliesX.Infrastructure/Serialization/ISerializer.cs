namespace WooliesX.Infrastructure.Serialization
{
    public interface ISerializer
    {
        string Serialize(object data);
        T Deserialize<T>(string value);
    }
}
