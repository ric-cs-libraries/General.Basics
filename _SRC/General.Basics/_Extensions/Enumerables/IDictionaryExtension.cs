using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using General.Basics.ErrorHandling;


namespace General.Basics.Extensions;

public static class IDictionaryExtension
{
    public static string ToKeyValueString_<V>(this IDictionary<string, V> dictionary, string keyValueSeparator = ";", string quoteValueSymbol = "", string keyValueEqualitySymbol = "=") 
    {
        var retour = string.Empty;
        
        ICollection<string> strings = new Collection<string>();
        string str;
        foreach (var keyValue in dictionary)
        {
            str = $"{keyValue.Key}{keyValueEqualitySymbol}{quoteValueSymbol}{keyValue.Value}{quoteValueSymbol}";
            strings.Add(str);
        }

        retour = string.Join(keyValueSeparator, strings);

        return retour;
    }

    public static void CheckKeysExist_<K, V>(this IDictionary<K, V> dictionary, IEnumerable<K> keys, string subject = "Dictionary")
    {
        foreach (var key in keys)
        {
            dictionary.CheckKeyExists_(key, subject);
        }
    }

    /// <exception cref="UnfoundKeyException"></exception>
    public static void CheckKeyExists_<K, V>(this IDictionary<K, V> dictionary, K key, string subject = "Dictionary")
    {
        if (!dictionary.TryGetValue(key, out V? dummy))
        {
            throw new UnfoundKeyException(key, subject);
        }
    }


    public static bool KeysAreExactly_<K, V>(this IDictionary<K, V> dictionary, IEnumerable<K> allKeysToFind, IEqualityComparer<K>? comparer = null)
    {
        ICollection<K> keys = dictionary.Keys;
        var response = keys.ContainsSameElementsAs_(allKeysToFind, comparer);
        return response;
    }

}