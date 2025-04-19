using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using General.Basics.ReflectionExtended.POO.Abstracts;

namespace General.Basics.ReflectionExtended.POO;

public record Interface : BasicElement
{
    public Interface(Type type) : base(type)
    {
    }

    /// <exception cref="TypeShouldBeAnInterfaceTypeException"></exception>
    protected override void Validate(Type type)
    {
        if (!type.IsInterface)
        {
            throw new TypeShouldBeAnInterfaceTypeException(type.GetFullName_());
        }
    }
}
