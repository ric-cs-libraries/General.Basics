using General.Basics.Extensions;
using General.Basics.Reflection.Extensions;

using General.Basics.ReflectionExtended.Extensions;


namespace General.Basics.ReflectionExtended.DynamicCalls.Abstracts;

public class UnexistingMethodException : Exception
{
    public const string MESSAGE_FORMAT = "(In assembly '{0}'), Unexisting method : {1}({2}) .{3}";
    public const string TYPE_FOR_NULL_PARAMS = "Nullable<??>";

    public override string Message { get; }

    public UnexistingMethodException(Type type, string methodName, IEnumerable<Type?> parametersType, string errorDetails = "")
    {
        var assemblyName = type.GetAssemblyName_();

        IEnumerable<string> parametersTypeList = parametersType.Select(type => type?.GetFullName_() ?? TYPE_FOR_NULL_PARAMS);
        string parametersTypeList_ = string.Join(", ", parametersTypeList);

        string methodFullName = string.Join(".", new string[] { type.GetFullName_(), methodName });

        errorDetails = errorDetails.Trim();
        if (!errorDetails.IsEmpty_())
        {
            errorDetails = $" Detail : {errorDetails}.";
        }

        Message = string.Format(MESSAGE_FORMAT, assemblyName, methodFullName, parametersTypeList_, errorDetails);
    }
}
