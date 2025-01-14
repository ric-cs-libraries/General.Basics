namespace General.Basics.Reflection.POO.Extensions;

public static partial class TypeExtension
{
    //ATTENTION: IsOfType_ ne prend pas en compte l'héritage, ou l'implém. d'interfaces.
    //           Utiliser type.BaseType au lieu de type, peut alors éventuellement être une solution.
    public static bool IsOfType_(this Type type, Type ofType)
    {
        //Utile pour tester ce genre de chose (avec type = thisType, et ofType = typeof(MaClasse<>)) :
        //                    typeof(MyClasse<>).IsAssignableFrom(thisType)   qui renvoie malheureusement toujours false <<<<<
        // C-à-d qu'ici on veut renvoyer true SI thisType est >EXACTEMENT< LE type MyClasse<T,...> QUEL QUE SOIT  T,...  en fait !!

        bool response = type.FullName is not null && ofType.FullName is not null
                        && type.FullName!.StartsWith(ofType.FullName!);
        return response;
    }

    public static bool Implements_(this Type type, Type interfaceType)
    {
        bool response = type.IsClass && interfaceType.IsInterface && interfaceType.IsAssignableFrom(type);
        return response;
    }
}

