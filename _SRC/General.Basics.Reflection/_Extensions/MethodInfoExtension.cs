using System.Reflection;

using General.Basics.Extensions;



namespace General.Basics.Reflection.Extensions;

public static class MethodInfoExtension
{
    public static string GetFullName_(this MethodInfo method, bool fullInfos = false)
    {
        Type methodOwnerType = method.DeclaringType!;
        var result = $"{method.ReturnType.GetFullName_()} {methodOwnerType.GetFullName_()}.{method.Name}({method.GetParametersList_(fullInfos)})";
        return result;
    }

    public static string GetParametersList_(this MethodInfo method, bool fullInfos)
    {
        List<string> parametersList = new();

        ParameterInfo[] methodParameters = method.GetParameters();

        string parameterInfos;
        foreach (ParameterInfo parameterInfo in methodParameters)
        {
            parameterInfos = (fullInfos) ? parameterInfo.GetFullInfo_() : parameterInfo.GetTypeInfo_();
            parametersList.Add(parameterInfos);
        }

        string result = string.Join(", ", parametersList);
        return result;
    }

    public static bool IsReturnTypeATask_(this MethodInfo method) => typeof(Task).IsAssignableFrom(method.ReturnType);
}