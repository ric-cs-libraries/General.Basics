using General.Basics.Reflection.Static;

namespace General.Basics.Reflection.Extensions;

public static partial class TypeExtension
{
    public static string GetSimpleName_(this Type type)
    {
        var excludeFromThisChar = Type_.GENERIC_TYPES_NAME_SEPARATOR_SYMBOL; //Caractère "gênant" ajouté dans la GetType().Name, lorsque la classe est générique.
        var result = $"{type.Name.Split(excludeFromThisChar)[0]}";
        return result;
    }

    public static string GetSimpleFullName_(this Type type)
    {
        var result = $"{type.Namespace}.{type.GetSimpleName_()}";

        return result;
    }

    public static string GetName_(this Type type)
    {
        string name = null!;

        if (type.IsGenericType)
        {
            Type[] genericParametersType = type.GenericTypeArguments;
            List<string> genericParameterTypeNames = new();
            string genericParameterTypeName = null!;
            foreach (Type genericParameterType in genericParametersType)
            {
                genericParameterTypeName = genericParameterType.GetName_();
                genericParameterTypeNames.Add(genericParameterTypeName);
            }
            name = $"{type.GetSimpleName_()}<{string.Join(",", genericParameterTypeNames)}>";
        }
        else
        {
            name = type.Name; //Plutôt que GetFullName_(), pour plus de lisibilité.
        }
        return name;
    }

    public static string GetFullName_(this Type type)
    {
        var result = $"{type.Namespace}.{type.GetName_()}";

        return result;
    }
}
