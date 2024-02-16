using Newtonsoft.Json;

using General.Basics.Serialization.Interfaces;

namespace General.Basics.Serialization;

public class NewtonSoftSerializer : ISerializer
{
    private NewtonSoftSerializer()
    {

    }

    public static NewtonSoftSerializer Create() 
    {
      var result = new NewtonSoftSerializer();
      return result;
    }

    public string ToJson(object data, bool indented = false)
    {
        Formatting formatting = (indented) ? Formatting.Indented : Formatting.None;
        var options = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore //IGNORE le membre pointant vers une référence déjà scannée
        };

        var result = JsonConvert.SerializeObject(data, formatting, options);

        return result;
    }

    public T? FromJson<T>(string jsonString)
    {
        var result = JsonConvert.DeserializeObject<T>(jsonString);
        return result;
    }
}
