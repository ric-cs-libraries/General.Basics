using General.Basics.Extensions;

namespace General.Basics.Reflection.Extensions;

//Méthodes static en lien avec la classe : Type.
public static class Type_
{
    public const string GENERIC_TYPES_NAME_SEPARATOR_SYMBOL = "`";


    //Dans cette méthode, quand je dis "class", j'entends aussi bien : classe, interface, record, que struct.
    //Méthode typiquement utile, pour utilisation de : Activate.CreateInstance(...).
    public static Type? GetTypeFromNamesInfos(string classAssemblyName, string classNamespace, string className)
    {
        string classNamespaceAndName = $"{classNamespace}.{className}";
        string assemblyPartialQualifiedName = $"{classNamespaceAndName}, {classAssemblyName}"; //un assemblyQualifiedName complet, est plus long que ça (plus d'infos).

        Type? classAsType = Type.GetType(assemblyPartialQualifiedName); //<<<<<<<<<<<<<<<<<<<<<<<<<<

        return classAsType;
    }

    //Dans cette méthode, quand je dis "class", j'entends aussi bien : classe, interface, record, que struct.
    //Méthode typiquement utile, pour utilisation de : Activate.CreateInstance(...).
    // REM.: genericClassName : nom de la classe générique, SANS chevron...         <<<<<<
    public static Type? GetGenericTypeFromNamesInfos(string genericClassAssemblyName, string genericClassNamespace, string genericClassName, Type[] genericParametersType)
    {
        int nbGenericParameters = genericParametersType.Length;
        if (nbGenericParameters == 0)
        {
            throw new MissingGenericParametersTypeException($"{nameof(Type_)}.{nameof(Type_.GetGenericTypeFromNamesInfos)}", $"{genericClassNamespace}.{genericClassName}");
        }

        genericClassName = genericClassName.EndsWith_(true, $"{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{nbGenericParameters}"); //C'est ainsi que .NET nomme une classe générique (myGenericType.Name).
        //genericClassName = $"{genericClassName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{nbGenericParameters}"; //C'est ainsi que .NET nomme une classe générique (myGenericType.Name).


        Type? classAsType = Type_.GetTypeFromNamesInfos(genericClassAssemblyName, genericClassNamespace, genericClassName);


        classAsType = classAsType?.MakeGenericType(genericParametersType!); //Traitement particulier pour les types génériques  <<<<<<<<<<<<<<

        return classAsType;
    }

}
