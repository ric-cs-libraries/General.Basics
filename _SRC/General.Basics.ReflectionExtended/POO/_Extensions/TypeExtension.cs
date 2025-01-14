using General.Basics.ReflectionExtended.POO.Abstracts;
using General.Basics.ReflectionExtended.POO.ErrorHandling;

namespace General.Basics.ReflectionExtended.POO.Extensions;

public static partial class TypeExtension
{
    public static bool Implements_(this Type type, Interface @interface)
    {
        bool result = type.IsClass && @interface.Type.IsAssignableFrom(type);
        return result;
    }

    //classOrInterface : can describe a record or a class or an interface
    public static bool InheritsFrom_(this Type type, BasicElement classOrInterface)
    {
        return InheritsFrom(type, classOrInterface.Type);
    }

    //classOrInterfaceType :  can describe a record or a class or an interface
    private static bool InheritsFrom(Type type, Type classOrInterfaceType)
    {
        bool result = false;
        if (classOrInterfaceType.IsClass && type.IsClass || classOrInterfaceType.IsInterface && type.IsInterface)
        {
            result = classOrInterfaceType.IsAssignableFrom(type);

        }
        return result;
    }

    public static void CheckIfRedefinesOneOfTheseMethods_(this Type concreteType, IEnumerable<string> methodsName, Type parentTypeThatDefinesThemAll)
    {
#if DEBUG
        bool isOneMethodOverridedInConcreteClass = concreteType.RedefinesOneOfTheseMethods_(methodsName, parentTypeThatDefinesThemAll);

        if (!isOneMethodOverridedInConcreteClass)
            throw new ConcreteClassMustRedefineAtLeastOneSomeMethodsException(concreteType, methodsName, parentTypeThatDefinesThemAll)
        ;
#endif
    }

    public static bool RedefinesOneOfTheseMethods_(this Type concreteType, IEnumerable<string> methodsName, Type parentTypeThatDefinesThemAll)
    {
#if DEBUG
        bool isOneMethodOverridedInConcreteClass = false;
        foreach (var methodName in methodsName)
        {
            isOneMethodOverridedInConcreteClass = concreteType.GetMethod(methodName)!.DeclaringType != parentTypeThatDefinesThemAll;
            if (isOneMethodOverridedInConcreteClass)
                break
            ;

        }

        return isOneMethodOverridedInConcreteClass;
#else
        return true;
#endif
    }

}

