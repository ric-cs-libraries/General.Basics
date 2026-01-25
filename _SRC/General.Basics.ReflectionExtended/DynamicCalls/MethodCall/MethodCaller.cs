using General.Basics.ReflectionExtended.DynamicCalls.Interfaces;
using General.Basics.ReflectionExtended.Extensions;
using System.Reflection;
using General.Basics.ReflectionExtended.DynamicCalls.Abstracts;

namespace General.Basics.ReflectionExtended.DynamicCalls;


/// <summary>
/// --- Ne peut appeler QUE des méthodes NON static ! ---
/// </summary>
public class MethodCaller : Abstracts.MethodCaller, IMethodCaller
{
    public static MethodCaller Create()
    {
        return new();
    }

    /// <exception cref="CannotExpectAnyReturnedValueFromAVoidMethodException"></exception>
    /// <exception cref="UnexistingMethodException"></exception>
    public TReturnType? Call<TReturnType>(object obj, string methodName, object?[]? parameters = null)
    {
        MethodInfo method = GetMethodInfos(obj, methodName, parameters);
        if (IsSignatureWithVoidReturnType(method))
        {
            throw new CannotExpectAnyReturnedValueFromAVoidMethodException(method.GetFullName_());
        }

        object? returnedValue = null;
        try
        {
            returnedValue = method.Invoke(obj, parameters); //<<<
        }
        catch (ArgumentException ex)
        {
            ThrowUnexistingMethodException(obj.GetType(), methodName, parameters, ex.Message);
        }

        TReturnType? returnedValue_ = GetReturnedValue<TReturnType>(returnedValue);
        return returnedValue_;
    }

    /// <exception cref="UnexistingMethodException"></exception>
    public void Call(object obj, string methodName, object?[]? parameters = null)
    {
        MethodInfo method = GetMethodInfos(obj, methodName, parameters);

        try
        {
            method.Invoke(obj, parameters); //<<<
        }
        catch (ArgumentException ex)
        {
            ThrowUnexistingMethodException(obj.GetType(), methodName, parameters, ex.Message);
        }
    }

    private bool IsSignatureWithVoidReturnType(MethodInfo method)
    {
        var result = (method.ReturnType == typeof(void));
        return result;
    }
}
