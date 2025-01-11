namespace General.Basics.ErrorHandling;

public record AuthenticationError : Error
{
    public AuthenticationError(string code, string debugMessageTemplate = "Authentication Error", IEnumerable<string>? placeholderValues = null) 
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }
}
