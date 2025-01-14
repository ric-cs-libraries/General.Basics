using General.Basics.ReflectionExtended.POO.Abstracts;

namespace General.Basics.ReflectionExtended.POO;

public class TypeShouldBeAClassTypeException : TypeShouldBeException
{
    public const string EXPECTED_TYPE_LABEL = "a Class";

    public TypeShouldBeAClassTypeException(string typeFullName) : base(typeFullName, EXPECTED_TYPE_LABEL)
    {
    }
}