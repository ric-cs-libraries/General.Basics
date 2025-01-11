using General.Basics.ReflectionExtended.POO.Abstracts;

namespace General.Basics.ReflectionExtended.POO;

public class TypeShouldBeAnAbstractClassTypeException : TypeShouldBeException
{
    public const string EXPECTED_TYPE_LABEL = "an Abstract Class";

    public TypeShouldBeAnAbstractClassTypeException(string typeFullName) : base(typeFullName, EXPECTED_TYPE_LABEL)
    {
    }
}