using General.Basics.Reflection.Extensions;

namespace General.Basics.Reflection.POO;


//Concerne class et record.
public record AbstractClass : Class
{
	public AbstractClass(Type type) : base(type)
	{
	}

    protected override void Validate(Type type)
    {
        if (!(type.IsClass && type.IsAbstract))
        {
            throw new TypeShouldBeAnAbstractClassTypeException(type.GetFullName_());
        }
    }
}
