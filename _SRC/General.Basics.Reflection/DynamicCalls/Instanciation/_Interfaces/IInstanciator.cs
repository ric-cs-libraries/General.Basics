using General.Basics.Reflection.POO;


namespace General.Basics.Reflection.DynamicCalls.Interfaces;


//Ici, quand je dis "class", j'entends aussi bien : classe, record, que struct.
public interface IInstanciator
{
    object GetInstance(Class concreteClass, object[]? constructorParams = null);

    object GetInstance(string classAssemblyName, string classNamespace, string className, object[]? constructorParams = null);
}
