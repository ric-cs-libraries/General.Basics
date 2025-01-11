using System.Reflection;



namespace General.Basics.ReflectionExtended.DynamicCalls.Abstracts;


//--- Ne fonctionne que sur des méthodes NON static ! ---
public abstract class MethodCaller
{
    internal protected TReturnType? GetReturnedValue<TReturnType>(object? returnedValue)
    {
        if (returnedValue is not null)
        {
            Type effectiveReturnedType = returnedValue.GetType();
            Type expectedReturnedType = typeof(TReturnType);
            if (!expectedReturnedType.IsAssignableFrom(effectiveReturnedType))
            {
                throw new MismatchingReturnedValueTypeForThisCallException(expectedReturnedType, effectiveReturnedType);
            }
            return (TReturnType)returnedValue;
        }

        return default(TReturnType); //null
    }

    internal protected virtual MethodInfo GetMethodInfos(object obj, string methodName, object?[]? parameters)
    {
        Type objectType = obj.GetType();

        MethodInfo? method = objectType.GetMethod(methodName);
        if (method is null)
        {
            ThrowUnexistingMethodException(objectType, methodName, parameters);
        }
        return method!;
    }

    internal protected void ThrowUnexistingMethodException(Type objectType, string methodName, object?[]? parameters, string errorDetails = "")
    {
        IEnumerable<Type?> parametersType = parameters?.Select(param => param?.GetType()) ?? Enumerable.Empty<Type?>();
        throw new UnexistingMethodException(objectType, methodName, parametersType, errorDetails);
    }
}
