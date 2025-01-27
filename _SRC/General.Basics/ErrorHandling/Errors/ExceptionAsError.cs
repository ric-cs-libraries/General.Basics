
using General.Basics.Reflection.Extensions;

namespace General.Basics.ErrorHandling;

public record ExceptionAsError : Error
{
    public ExceptionAsError(Exception exception) : this(exception, prefixMessage: string.Empty)
    {
    }

    public ExceptionAsError(Exception exception, string prefixMessage) : base(code: exception.GetType().GetName_(), debugMessageTemplate: $"{prefixMessage}{exception.Message}", placeholderValues: null)
    {
    }
}
