using General.Basics.ReflectionExtended.POO.Abstracts;

namespace General.Basics.ReflectionExtended.POO;

public class TypeShouldBeAnInterfaceTypeException : TypeShouldBeException
{
    public const string EXPECTED_TYPE_LABEL = "an Interface";

    public TypeShouldBeAnInterfaceTypeException(string typeFullName) : base(typeFullName, EXPECTED_TYPE_LABEL)
    {
    }
}