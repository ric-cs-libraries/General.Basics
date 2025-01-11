using General.Basics.Reflection.Extensions;
using System.Reflection;


namespace General.Basics.ReflectionExtended.Extensions;

public static class ParameterInfoExtension
{
    public static string GetTypeInfo_(this ParameterInfo parameter)
    {
        var result = parameter.ParameterType.GetFullName_();
        return result;
    }

    public static string GetFullInfo_(this ParameterInfo parameter)
    {
        List<string> parameterInfos = new();

        string parameterType, parameterOption, parameterName;

        if (parameter.IsOptional)
        {
            parameterOption = "Optional";
            parameterInfos.Add(parameterOption);
        }

        parameterType = parameter.GetTypeInfo_();
        parameterInfos.Add(parameterType);

        parameterName = parameter.Name!;
        parameterInfos.Add(parameterName);

        var result = string.Join(" ", parameterInfos);
        return result;
    }
}
