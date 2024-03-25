using General.Basics.Reflection.POO.Abstracts;

namespace General.Basics.Reflection.POO;

public class TypeShouldBeAnInterfaceTypeException : TypeShouldBeException
{
    public const string EXPECTED_TYPE_LABEL = "an Interface";

    public TypeShouldBeAnInterfaceTypeException(string typeFullName) : base(typeFullName, EXPECTED_TYPE_LABEL)
    {
    }
}