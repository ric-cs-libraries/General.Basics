using General.Basics.ReflectionExtended.DynamicCalls.Interfaces;
using General.Basics.ReflectionExtended.Extensions;
using System.Reflection;


namespace General.Basics.ReflectionExtended.DynamicCalls;


//--- Ne peut appeler QUE des méthodes NON static ! ---
public class MethodCallerAsync : Abstracts.MethodCaller, IMethodCallerAsync
{
    public static MethodCallerAsync Create()
    {
        return new();
    }

    //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
    //La méthode à invoquer devra retourner une : Task<TReturnType?> .
    public async Task<TReturnType?> Call<TReturnType>(object obj, string methodName, object?[]? parameters)
    {
        MethodInfo method = GetMethodInfos(obj, methodName, parameters);

        object? returnedValue = null;
        try
        {
            object? returnedTask = method.Invoke(obj, parameters);
            Task<TReturnType?>? task = returnedTask as Task<TReturnType?>;
            if (task is null)
            {
                throw new AppropriateAwaitableExpectedAsReturnedTypeException(effectiveReturnedType: method.ReturnType, expectedReturnedValueType: typeof(TReturnType?));
            }
            returnedValue = await task!;

        }
        catch (ArgumentException ex)
        {
            ThrowUnexistingMethodException(obj.GetType(), methodName, parameters, ex.Message);
        }

        TReturnType? returnedValue_ = GetReturnedValue<TReturnType>(returnedValue);
        return returnedValue_;
    }

    //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
    //La méthode à invoquer devra retourner une : Task.
    public async Task Call(object obj, string methodName, object?[]? parameters)
    {
        MethodInfo method = GetMethodInfos(obj, methodName, parameters);

        try
        {
            object? returnedTask = method.Invoke(obj, parameters);
            Task task = (returnedTask as Task)!;
            await task!;

        }
        catch (ArgumentException ex)
        {
            ThrowUnexistingMethodException(obj.GetType(), methodName, parameters, ex.Message);
        }
    }

    internal protected override MethodInfo GetMethodInfos(object obj, string methodName, object?[]? parameters)
    {
        MethodInfo method = base.GetMethodInfos(obj, methodName, parameters);
        if (IsSignatureWithoutTaskAsReturnType(method))
        {
            throw new AwaitableExpectedAsReturnedTypeException(method.GetFullName_());
        }

        return method!;
    }

    private bool IsSignatureWithoutTaskAsReturnType(MethodInfo method)
    {
        var result = !method.IsReturnTypeATask_();
        return result;
    }
}
