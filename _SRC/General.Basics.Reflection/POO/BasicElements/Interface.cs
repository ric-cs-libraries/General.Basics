using General.Basics.Reflection.POO.Abstracts;

namespace General.Basics.Reflection.POO;

public record Interface : BasicElement
{
	public Interface(Type type) : base(type)
	{
	}

    protected override void Validate(Type type)
    {
        if (!type.IsInterface)
        {
            throw new TypeShouldBeAnInterfaceTypeException(type.Name);
        }
    }
}
