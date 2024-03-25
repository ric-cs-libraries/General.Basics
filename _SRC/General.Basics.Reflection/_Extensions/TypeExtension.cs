using General.Basics.Extensions;
using General.Basics.Reflection.POO;
using General.Basics.Reflection.POO.Abstracts;

namespace General.Basics.Reflection.Extensions;


public static class TypeExtension
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

    public static string GetAssemblyName_(this Type type)
    {
        string result = type.Module.Name.EndsWith_(false, ".dll");
        return result;
    }

    public static bool Implements_(this Type type, Interface @interface)
    {
        bool result = type.IsClass && @interface.Type.IsAssignableFrom(type);
        return result;
    }

    //classOrInterface : can describe a record or a class or an interface
    public static bool InheritsFrom_(this Type type, BasicElement classOrInterface)
    {
        return InheritsFrom(type, classOrInterface.Type);
    }

    //classOrInterfaceType :  can describe a record or a class or an interface
    private static bool InheritsFrom(Type type, Type classOrInterfaceType)
    {
        bool result = false;
        if ( (classOrInterfaceType.IsClass && type.IsClass) || (classOrInterfaceType.IsInterface && type.IsInterface) )
        {
            result = classOrInterfaceType.IsAssignableFrom(type);

        }
        return result;
    }
}
