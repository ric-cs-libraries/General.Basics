using General.Basics.Reflection.Extensions;

using General.Basics.ReflectionExtended.DynamicCalls.Interfaces;
using General.Basics.ReflectionExtended.Extensions;
using General.Basics.ReflectionExtended.POO;


namespace General.Basics.ReflectionExtended.DynamicCalls;

public class InstanciatorForGeneric : Abstracts.Instanciator, IInstanciatorForGeneric
{
    public static InstanciatorForGeneric Create()
    {
        return new();
    }

    //Throws .NET MissingMethodException : when constructor params type, doesn't match any possible constructor.
    public object GetInstance(Class concreteGenericClass, Type[] genericParametersType, object[]? constructorParams = null)
    {
        string genericClassAssemblyName = concreteGenericClass.Type.GetAssemblyName_();
        string genericClassNamespace = concreteGenericClass.Type.Namespace!;
        string genericClassName = concreteGenericClass.Type.GetSimpleName_();

        object instance = GetInstance(genericClassAssemblyName, genericClassNamespace, genericClassName, genericParametersType, constructorParams);
        return instance;
    }

    //Dans cette méthode, quand je dis "class", j'entends aussi bien : classe, record, que struct.
    //Throws .NET MissingMethodException : when constructor params type, doesn't match any possible constructor.
    public object GetInstance(string genericClassAssemblyName, string genericClassNamespace, string genericClassName,
                                      Type[] genericParametersType, object[]? constructorParams = null)
    {
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericClassAssemblyName, genericClassNamespace, genericClassName, genericParametersType);
        if (type is null || !type.IsGenericType)
        {
            throw new CheckIfIsGenericTypeException($"{genericClassNamespace}.{genericClassName}");
        }

        object instance = GetInstance(type!, constructorParams);
        return instance;
    }
}
