using General.Basics.ReflectionExtended.POO.Abstracts;
using General.Basics.ReflectionExtended.POO.ErrorHandling;

namespace General.Basics.ReflectionExtended.POO;

public static partial class TypeExtension
{
    public static bool IsOfType_(this Type type, Type ofType) //ATTENTION: ne prend pas en compte l'héritage ou l'implém. d'interfaces.
    {
        //Utile pour tester ce genre de chose (avec type = thisType, et ofType = typeof(MaClasse<>)) :
        //                    typeof(MyClasse<>).IsAssignableFrom(thisType)   qui renvoie malheureusement toujours false <<<<<
        // C-à-d qu'ici on veut renvoyer true SI thisType est EXACTEMENT de type MyClasse<T,...> QUEL QUE SOIT  T,...  en fait !!

        bool response = type.FullName is not null && ofType.FullName is not null
                        && type.FullName!.StartsWith(ofType.FullName!);
        return response;
    }

    public static bool Implements_(this Type type, Type interfaceType)
    {
        bool response = type.IsClass && interfaceType.IsInterface_() && interfaceType.IsAssignableFrom(type);
        return response;
    }

    public static bool Implements_(this Type type, Interface @interface)
    {
        bool result = type.IsClass && @interface.Type.IsAssignableFrom(type);
        return result;
    }

    public static bool IsInterface_(this Type type)
    {
        bool response = !type.IsClass && type.IsAbstract;
        return response;
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
        if ((classOrInterfaceType.IsClass && type.IsClass) || (classOrInterfaceType.IsInterface && type.IsInterface))
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

