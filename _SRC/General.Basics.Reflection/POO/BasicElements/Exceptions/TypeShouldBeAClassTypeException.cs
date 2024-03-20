using General.Basics.Reflection.POO.Abstracts;

namespace General.Basics.Reflection.POO;

public class TypeShouldBeAClassTypeException : TypeShouldBeException
{
    public const string EXPECTED_TYPE_LABEL = "a Class";

    public TypeShouldBeAClassTypeException(string typeName) : base(typeName, EXPECTED_TYPE_LABEL)
    {
    }
}