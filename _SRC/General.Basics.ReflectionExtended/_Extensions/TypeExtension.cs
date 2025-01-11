using General.Basics.Extensions;


namespace General.Basics.ReflectionExtended.Extensions;

public static partial class TypeExtension
{
    public static bool IsMicrosoftType_(this Type type)
    {
        bool response = type.Namespace?.StartsWith("Microsoft.") ?? false;
        return response;
    }

    public static string GetAssemblyName_(this Type type)
    {
        string result = type.Module.Name.EndsWith_(false, ".dll");
        return result;
    }

}
