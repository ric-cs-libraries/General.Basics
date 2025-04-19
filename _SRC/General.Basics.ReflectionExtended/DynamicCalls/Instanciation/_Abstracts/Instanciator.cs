
namespace General.Basics.ReflectionExtended.DynamicCalls.Abstracts;

public abstract class Instanciator
{
    /// <exception cref="AnAbstractTypeCannotBeInstanciatedException"></exception>
    /// <exception cref="MissingMethodException">Throws .NET MissingMethodException : when constructor params type, doesn't match any possible constructor</exception>
    protected internal object GetInstance(Type type, object[]? constructorParams = null)
    {
        if (type.IsAbstract)
        {
            throw new AnAbstractTypeCannotBeInstanciatedException($"{type.Namespace}.{type.Name}");
        }

        object instance = Activator.CreateInstance(type!, constructorParams)!;
        return instance;
    }
}
