namespace Transverse._Common.General.Serialization.Interfaces;


public interface ISerializer
{
    string ToJson(object data, bool indented = false);

    T? FromJson<T>(string jsonString);
}    