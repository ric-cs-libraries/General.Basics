using General.Basics.ReflectionExtended.POO;


namespace General.Basics.ReflectionExtended.DynamicCalls.Interfaces;


//Ici, quand je dis "class", j'entends aussi bien : classe, record, que struct.
public interface IInstanciatorForGeneric
{
    object GetInstance(Class concreteGenericClass, Type[] genericParametersType, object[]? constructorParams = null);

    object GetInstance(string genericClassAssemblyName, string genericClassNamespace, string genericClassName,
                       Type[] genericParametersType, object[]? constructorParams = null);
}
