using General.Basics.ReflectionExtended.POO;


namespace General.Basics.ReflectionExtended.DynamicCalls.Interfaces;


//Ici, quand je dis "class", j'entends aussi bien : classe, record, que struct.
public interface IInstanciator
{
    object GetInstance(Class concreteClass, object[]? constructorParams = null);

    object GetInstance(string classAssemblyName, string classNamespace, string className, object[]? constructorParams = null);
}
