using System.Text.Json; 
using System.Text.Json.Serialization; //Pour ReferenceHandler
using System.Text.Encodings.Web; //pour JavaScriptEncoder
using System.Text.Unicode; //pour UnicodeRanges

using Transverse._Common.General.Serialization.Interfaces;


namespace Transverse._Common.General.Serialization;


public class NativeSerializer : ISerializer
{
    private NativeSerializer()
    {

    }

    public static NativeSerializer Create() 
    {
      var result = new NativeSerializer();
      return result;
    }

    public string ToJson(object data, bool indented = false)
    {
        var options = new JsonSerializerOptions() 
        {
            WriteIndented = indented,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), //pour ne pas avoir de souci de jeu de caractères
            ReferenceHandler = ReferenceHandler.IgnoreCycles //Garde le membre qui pointait vers une référence déjà scannée mais lui donne la valeur null  (existe depuis .net6)
            //ReferenceHandler = ReferenceHandler.Preserve //Garde le membre qui pointe vers une référence déjà scannée mais lui donne une valeur $id, correspondant 
                                                          //a un id virtuellement ajouté à l'objet pointé   (existe depuis .net6)
        };

        var result = JsonSerializer.Serialize(data, options); //System.Text.Json

        return result;
    }

    public T? FromJson<T>(string jsonString)
    {
        var result = JsonSerializer.Deserialize<T>(jsonString);
        return result;
    }
}
