namespace General.Basics.Exceptions;


public class StringWithIllegalCharException : Exception
{
	public const string MESSAGE_FORMAT = "The string '{0}' contains an illegal char : '{1}'.";
	public override string Message { get; }

	public StringWithIllegalCharException(string str, char illegalCharacterFound)
	{
		Message = string.Format(MESSAGE_FORMAT, str, illegalCharacterFound);
	}
}
