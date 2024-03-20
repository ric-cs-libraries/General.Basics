using System.Reflection;


using General.Basics.Reflection.Extensions;
using General.Basics.Reflection.DynamicCalls.Interfaces;
using General.Basics.Reflection.DynamicCalls.Abstracts;


namespace General.Basics.Reflection.DynamicCalls;


//--- Ne peut appeler QUE des méthodes NON static ! ---
public class MethodCaller  : Abstracts.MethodCaller, IMethodCaller
{
    public static MethodCaller Create()
    {
        return new();
    }

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
