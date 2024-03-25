using System.Reflection;
using General.Basics.Reflection.POO;
using General.Basics.Reflection.DynamicCalls;
using General.Basics.Reflection.DynamicCalls.Interfaces;



namespace General.Basics.Reflection.Extensions;

public static class AssemblyExtension
{
    #region Reflection
    public static List<Class> GetClassesImplementingTheInterface_(this Assembly assembly, Interface @interface)
    {
        List<Class> classes = GetClassesImplementingTheInterface(assembly, @interface);
        return classes;
    }
    public static List<Class> GetConcreteClassesImplementingTheInterface_(this Assembly assembly, Interface @interface)
    {
        List<Class> classes = GetClassesImplementingTheInterface(assembly, @interface, type => !type.IsAbstract);
        return classes;
    }
    private static List<Class> GetClassesImplementingTheInterface(Assembly assembly, Interface @interface, Func<Type, bool>? additionalFilter = null)
    {
        IEnumerable<Type> types = assembly.GetTypes()
                                  .Where(type => type.Implements_(@interface));

        if (additionalFilter is not null)
        {
            types = types.Where(additionalFilter);
        }

        IEnumerable<Class> classes = types.Select(type => (type.IsAbstract) ? new AbstractClass(type) : new Class(type));

        return classes.ToList();
    }

    public static IReadOnlyList<TInterface> GetInstancesFromClassesImplementing_<TInterface>(this Assembly assembly, object[]? constructorParams = null)
    {
        Type typeOfInterface = typeof(TInterface);
        Interface @interface = new(typeOfInterface);

        IInstanciator instanciator = Instanciator.Create();

        IEnumerable<Class> classes = assembly.GetConcreteClassesImplementingTheInterface_(@interface)
                                             .Where((Class concreteClass) => !concreteClass.Type.IsGenericType)
                                             ;

        IEnumerable<TInterface> instances = classes
                                           .Select((Class concreteClass) => instanciator.GetInstance(concreteClass, constructorParams))
                                           .Cast<TInterface>();

        return instances.ToList();
    }

    public static IReadOnlyList<TInterface> GetInstancesFromGenericClassesImplementing_<TInterface>(this Assembly assembly, Type[] genericParametersType, object[]? constructorParams = null)
    {
        Type typeOfInterface = typeof(TInterface);
        Interface @interface = new(typeOfInterface);

        IInstanciatorForGeneric instanciatorForGeneric = InstanciatorForGeneric.Create();

        int nbGenericParameters = genericParametersType.Length;

        IEnumerable<Class> classes = assembly.GetConcreteClassesImplementingTheInterface_(@interface)
                                            .Where((Class concreteClass) => {
                                                bool b = concreteClass.Type.IsGenericType && concreteClass.Type.GetGenericArguments().Length == nbGenericParameters;
                                                return b;
                                             })
                                            ;

        IEnumerable<TInterface> instances = classes
                                           .Select((Class concreteGenericClass) => instanciatorForGeneric.GetInstance(concreteGenericClass, genericParametersType, constructorParams))
                                           .Cast<TInterface>();

        return instances.ToList();
    }
    #endregion Reflection
}
