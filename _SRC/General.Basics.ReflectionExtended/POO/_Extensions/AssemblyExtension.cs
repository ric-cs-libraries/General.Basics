using General.Basics.ReflectionExtended.DynamicCalls;
using General.Basics.ReflectionExtended.DynamicCalls.Interfaces;
using System.Reflection;


namespace General.Basics.ReflectionExtended.POO.Extensions;

public static class AssemblyExtension
{
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

        IEnumerable<Class> classes = types.Select(type => type.IsAbstract ? new AbstractClass(type) : new Class(type));

        return classes.ToList();
    }

    public static IReadOnlyList<TInterface> GetInstancesFromClassesImplementing_<TInterface>(this Assembly assembly, object[]? constructorParams = null)
    {
        Type typeOfInterface = typeof(TInterface);
        Interface @interface = new(typeOfInterface);

        IInstanciator instanciator = Instanciator.Create();

        IEnumerable<Class> classes = assembly.GetConcreteClassesImplementingTheInterface_(@interface)
                                             .Where((concreteClass) => !concreteClass.Type.IsGenericType)
                                             ;

        IEnumerable<TInterface> instances = classes
                                           .Select((concreteClass) => instanciator.GetInstance(concreteClass, constructorParams))
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
                                            .Where((concreteClass) =>
                                            {
                                                bool b = concreteClass.Type.IsGenericType && concreteClass.Type.GetGenericArguments().Length == nbGenericParameters;
                                                return b;
                                            })
                                            ;

        IEnumerable<TInterface> instances = classes
                                           .Select((concreteGenericClass) => instanciatorForGeneric.GetInstance(concreteGenericClass, genericParametersType, constructorParams))
                                           .Cast<TInterface>();

        return instances.ToList();
    }
}
