using General.Basics.Reflection.Extensions;

namespace General.Basics.ErrorHandling.Extensions;

public static partial class ExceptionExtension
{
    public static ExceptionAsError ToError(this Exception exception, string prefixMessage = "")
    {
        return new ExceptionAsError(exception, prefixMessage);
    }

    public static ExceptionAsError ToError(this Exception exception, Type instanceType, string methodName, string methodParams)
    {
        var prefixMessage = $"{instanceType.GetName_()}.{methodName}({methodParams}): ";
        return exception.ToError(prefixMessage);
    }

}
