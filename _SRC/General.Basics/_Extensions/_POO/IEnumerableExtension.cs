using General.Basics.Extensions.ErrorHandling;
using General.Basics.Reflection.Extensions;


namespace General.Basics.Extensions;

public static partial class IEnumerableExtension
{
    /// <exception cref="CannotDowncastException"></exception>
    public static IEnumerable<TChild> DowncastAll_<TParent, TChild>(this IEnumerable<TParent> children)
        where TChild : class, TParent
        where TParent : class
    {
        List<TChild> childrenToChildType = new();

        TChild childToChildType;
        var index = 0;
        foreach (var child in children)
        {
            childToChildType = (child as TChild)!;
            if (childToChildType is null)
                throw new CannotDowncastException(child.GetType().Name, typeof(TChild).Name, $"Item[{index}] in {children.GetType().GetName_()}");
            ;

            childrenToChildType.Add(childToChildType!);
            index++;
        }

        return childrenToChildType;
    }

    public static bool CanDowncastAll_<TParent, TChild>(this IEnumerable<TParent> children)
        where TChild : class, TParent
        where TParent : class
    {
        bool result = children.All(elem => ((elem as TChild) is not null));
        return result;
    }

    public static bool ContainsOnlyThoseConcreteTypes_<TParent>
        (this IEnumerable<TParent> children, IEnumerable<Type?> authorizedConcreteTypes)
        where TParent : class?
    {
        bool result = children.All(elem => authorizedConcreteTypes.Contains(elem?.GetType()));
        return result;
    }

}
