using General.Basics.Reflection.Extensions;
using General.Basics.ReflectionExtended.Extensions;
using General.Basics.ReflectionExtended.POO.Abstracts;

namespace General.Basics.ReflectionExtended.POO;

//Concerne class et record.
public record Class : BasicElement
{
    public Class(Type type) : base(type)
    {
    }

    /// <exception cref="TypeShouldBeAClassTypeException"></exception>
    protected override void Validate(Type type)
    {
        if (!type.IsClass)
        {
            throw new TypeShouldBeAClassTypeException(type.GetFullName_());
        }
    }
}
