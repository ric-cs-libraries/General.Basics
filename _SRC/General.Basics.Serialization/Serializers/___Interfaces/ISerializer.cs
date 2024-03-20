namespace General.Basics.Serialization.Serializers.Interfaces;


public interface ISerializer
{
    string ToJson(object data, bool indented = false);

    T? FromJson<T>(string jsonString);
}    