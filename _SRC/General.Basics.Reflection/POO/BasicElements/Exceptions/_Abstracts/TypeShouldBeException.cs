namespace General.Basics.Reflection.POO.Abstracts;

public abstract class TypeShouldBeException : Exception
{
    public const string MESSAGE_FORMAT = "The type '{0}' doesn't describe {1}, but it should.";

    public override string Message { get; } = null!;

    public TypeShouldBeException(string typeFullName, string expectedTypeKing)
    {
        Message = string.Format(MESSAGE_FORMAT, typeFullName, expectedTypeKing);
    }
}