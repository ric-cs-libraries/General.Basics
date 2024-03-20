using General.Basics.Reflection.POO.Abstracts;

namespace General.Basics.Reflection.POO;

//Concerne class et record.
public record Class : BasicElement
{
	public Class(Type type) : base(type)
    {
	}

    protected override void Validate(Type type)
    {
        if (!type.IsClass)
        {
            throw new TypeShouldBeAClassTypeException(type.Name);
        }
    }
}
