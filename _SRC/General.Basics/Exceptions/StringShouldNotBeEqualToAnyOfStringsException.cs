namespace General.Basics.Exceptions;


public class StringShouldNotBeEqualToAnyOfStringsException : Exception
{
    public const string MESSAGE_FORMAT = "With ComparisonMode='{0}', the string '{1}' must not be equal to one of these strings [{2}]";

    public override string Message { get; }

    public StringShouldNotBeEqualToAnyOfStringsException(StringComparison comparisonMode, string str, IEnumerable<string> strings)
    {
        var strings_ = $"'{string.Join("', '", strings)}'";
        var comparisonMode_ = Enum.GetName(typeof(StringComparison), comparisonMode);
        Message = string.Format(MESSAGE_FORMAT, comparisonMode_, str, strings_);
    }
}