using General.Basics.Extensions.ErrorHandling;


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
                throw new CannotDowncastException($"In {children.GetType().Name}[{index}]", child.GetType().Name, typeof(TChild).Name);
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

    public static bool ContainsOnlyTypes_<TParent>(this IEnumerable<TParent> children, IEnumerable<Type> authorizedTypes)
        where TParent : class
    {
        bool result = children.All(elem => authorizedTypes.Contains(elem?.GetType()));
        return result;
    }

}
