using General.Basics.ReflectionExtended.DynamicCalls.Interfaces;
using General.Basics.ReflectionExtended.Extensions;
using General.Basics.ReflectionExtended.POO;


namespace General.Basics.ReflectionExtended.DynamicCalls;

public class Instanciator : Abstracts.Instanciator, IInstanciator
{
    public static Instanciator Create()
    {
        return new();
    }

    /// <exception cref="MissingMethodException">Throws .NET MissingMethodException : when constructor params type, doesn't match any possible constructor</exception>
    public object GetInstance(Class concreteClass, object[]? constructorParams = null)
    {
        string classAssemblyName = concreteClass.Type.GetAssemblyName_();
        string classNamespace = concreteClass.Type.Namespace!;
        string className = concreteClass.Type.Name;

        object instance = GetInstance(classAssemblyName, classNamespace, className, constructorParams);
        return instance;
    }

    /// <summary>Dans cette méthode, quand je dis "class", j'entends aussi bien : classe, record, que struct.</summary>
    /// <exception cref="MissingMethodException">Throws .NET MissingMethodException : when constructor params type, doesn't match any possible constructor</exception>
    /// <exception cref="CheckIfIsNonGenericTypeException"></exception>
    public object GetInstance(string classAssemblyName, string classNamespace, string className, object[]? constructorParams = null)
    {
        Type? type = Type_.GetTypeFromNamesInfos(classAssemblyName, classNamespace, className);
        if (type is null || type.IsGenericType)
        {
            throw new CheckIfIsNonGenericTypeException($"{classNamespace}.{className}");
        }

        object instance = GetInstance(type!, constructorParams);
        return instance;
    }
}
