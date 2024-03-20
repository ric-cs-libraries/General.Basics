using General.Basics.Extensions;
using General.Basics.Reflection.POO;

namespace General.Basics.Reflection.Extensions;


public static class TypeExtension
{
    public static string GetSimpleName_(this Type type)
    {
        var excludeFromThisChar = Type_.GENERIC_TYPES_NAME_SEPARATOR_SYMBOL; //Caractère "gênant" ajouté dans la GetType().Name, lorsque la classe est générique.
        var result = $"{type.Name.Split(excludeFromThisChar)[0]}";
        return result;
    }
    [Obsolete("GetSimpleName is now Deprecated and soon will disappear, please use GetSimpleName_ instead.")]
    public static string GetSimpleName(this Type type)
    {
        return type.GetSimpleName_();
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

    //type : can describe a record or a class
    public static bool ClassImplements_(this Type type, Interface @interface)
    {
        return ClassImplements_(type, @interface.Type);
    }

    //type : can describe a record or a class
    public static bool ClassImplements_(this Type type, Type interfaceType)
    {
        bool result = false;
        if (interfaceType.IsInterface && type.IsClass)
        {
            result = type.GetInterfaces().Contains(interfaceType);

        }
        return result;
    }


    public static bool InheritsFrom_(this Type type, Interface @interface)
    {
        return InheritsFrom_(type, @interface.Type);
    }

    //@class : can describe a record or a class
    public static bool InheritsFrom_(this Type type, Class @class)
    {
        return InheritsFrom_(type, @class.Type);
    }

    //@classOrInterfaceType :  can describe a record or a class or an interface
    public static bool InheritsFrom_(this Type type, Type classOrInterfaceType)
    {
        bool result = false;
        if ( (classOrInterfaceType.IsClass && type.IsClass) || (classOrInterfaceType.IsInterface && type.IsInterface) )
        {
            result = classOrInterfaceType.IsAssignableFrom(type);

        }
        return result;
    }
}
