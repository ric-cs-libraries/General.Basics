
using General.Basics.Reflection.Extensions;

namespace General.Basics.ErrorHandling;

public record ExceptionAsError : ErrorWithOptionalCode
{
    public ExceptionAsError(Exception exception, string prefixMessage = "", string code = "")
        : base(code, debugMessageTemplate: $"({exception.GetType().GetName_()}) - {prefixMessage}{exception.Message}", placeholderValues: null)
    {
    }
}
