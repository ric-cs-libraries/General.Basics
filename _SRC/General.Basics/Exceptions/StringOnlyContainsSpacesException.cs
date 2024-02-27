namespace General.Basics.Exceptions;


public class StringOnlyContainsSpacesException : Exception
{
	public const string MESSAGE_FORMAT = "The string '{0}' must not only contain spaces.";
	public override string Message { get; }

	public StringOnlyContainsSpacesException(string str)
	{
		Message = string.Format(MESSAGE_FORMAT, str);
	}
}
