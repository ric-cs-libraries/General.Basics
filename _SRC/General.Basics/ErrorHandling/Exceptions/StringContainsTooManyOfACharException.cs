namespace General.Basics.ErrorHandling;


public class StringContainsTooManyOfACharException : Exception
{
    public const string MESSAGE_FORMAT = "The string '{0}' contains {1} times the char : '{2}', the max. authorized is : {3}.";
    public override string Message { get; }

    public StringContainsTooManyOfACharException(string str, int nbTimes, char theChar, int maxNbTimes)
    {
        Message = string.Format(MESSAGE_FORMAT, str, nbTimes, theChar, maxNbTimes);
    }
}
