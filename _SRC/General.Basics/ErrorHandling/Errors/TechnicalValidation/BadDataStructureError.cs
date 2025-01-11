namespace General.Basics.ErrorHandling;

public record BadDataStructureError : Error
{
    public BadDataStructureError(string code, string debugMessageTemplate = "Bad Data Structure", IEnumerable<string>? placeholderValues = null) 
        : base(code, debugMessageTemplate, placeholderValues)
    {
    }
}
