using Xunit;
using Moq;


using General.Basics.Reflection.Extensions;


using General.Basics.Reflection.DynamicCalls.Interfaces;
using General.Basics.Reflection.DynamicCalls.Abstracts;
using General.Basics.Reflection.DynamicCalls;


namespace General.Basics.Reflection.DynamicCalls.UnitTests;

public class MethodCallerAsyncTests
{
    private readonly IMethodCallerAsync methodCallerAsync;

    public MethodCallerAsyncTests()
    {
        methodCallerAsync = MethodCallerAsync.Create();
    }

    #region Call with Returned type
    #region Non generic class
    [Fact]
    public async Task CallWithReturnedType_WhenReturnedTypeIsValid_ShouldExecAndReturnThisValue()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "SomeStringMethod";

        object[] parameters1 = new object[] { "Hello", 50 };
        object?[] parameters2 = new object?[] { null, 20 };

        //--- Act ---
        var result1 = await methodCallerAsync.Call<string>(obj, methodName, parameters1);
        var result2 = await methodCallerAsync.Call<string>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.Equal($"{parameters1[0]}, {(int)parameters1[1] * 2}", result1);
        Assert.Equal($", {(int)parameters2[1]! * 2}", result2);
    }

    [Fact]
    public async Task CallWithReturnedType_WhenReturnedTypeIsAValidNullableString_ShouldExecAndReturnANullableString()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();  //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
        string methodName = "SomeNullableStringMethod"; //En effet l'interface ISomeInterfaceAsync ne possède pas de méthode SomeNullableStringMethod

        object[] parameters1 = new object[] { "Hello", 50 };
        object?[] parameters2 = new object?[] { "Negatif Donc retournera null", -20 };

        //--- Act ---
        string? result1 = await methodCallerAsync.Call<string?>(obj, methodName, parameters1);
        string? result2 = await methodCallerAsync.Call<string?>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.Equal($"{parameters1[0]}, {(int)parameters1[1] * 2}", result1);
        Assert.Null(result2);
    }

    [Fact]
    public async Task CallWithReturnedType_WhenReturnedTypeIsAValidNullableInstance_ShouldExecAndReturnThisANullableInstance()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();  //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
        string methodName = "GetSomeReturnable"; //En effet l'interface ISomeInterfaceAsync ne possède pas de méthode GetSomeReturnable

        object[] parameters1 = new object[] { false };
        object[] parameters2 = new object[] { true }; //la méthode retournera null

        //--- Act ---
        var result1 = await methodCallerAsync.Call<IReturnableAsync<string>?>(obj, methodName, parameters1);
        var result2 = await methodCallerAsync.Call<IReturnableAsync<string>?>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.NotNull(result1);
        Assert.IsType<SomeReturnableAsync>(result1);
        Assert.IsAssignableFrom<IReturnableAsync<string>>(result1);
        //Assert.Equal("Aa,Aa", await result1.GetValue("Aa"));
        Assert.Null(result2);
    }

    [Fact]
    public async Task CallWithReturnedType_WhenReturnedTypeValueIsNotTheExpectedOne_ShouldThrowAnAppropriateAwaitableExpectedAsReturnedTypeException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName1 = "SomeVoidMethod";
        string methodName2 = "SomeStringMethod";

        object[] parameters1 = new object[] { false };
        object[] parameters2 = new object[] { "Hi", 10 };

        //--- Act & Assert ---
        Task<AppropriateAwaitableExpectedAsReturnedTypeException> taskEx1 =
            Assert.ThrowsAsync<AppropriateAwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<DateTime>(obj, methodName1, parameters1));
        AppropriateAwaitableExpectedAsReturnedTypeException ex1 = await taskEx1;

        var expectedMessage1 = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            $"<{typeof(DateTime).GetFullName_()}>",
            typeof(Task).GetFullName_()
        );
        Assert.Equal(expectedMessage1, ex1.Message);

        //---
        Task<AppropriateAwaitableExpectedAsReturnedTypeException> taskEx2 =
            Assert.ThrowsAsync<AppropriateAwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<DateTime>(obj, methodName2, parameters2));
        AppropriateAwaitableExpectedAsReturnedTypeException ex2 = await taskEx2;

        var expectedMessage2 = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            $"<{typeof(DateTime).GetFullName_()}>",
            typeof(Task<string?>).GetFullName_()
        );
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public async Task CallWithReturnedType_WhenReturnedTypeIsNotAnAwaitable_ShouldThrowAnAwaitableExpectedAsReturnedTypeException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        Type classType = typeof(SomeClass);
        string methodName1 = "SomeVoidMethod";
        string methodName2 = "SomeStringMethod";

        object[] parameters1 = new object[] { false };
        object[] parameters2 = new object[] { "Hi", 10 };

        //--- Act & Assert ---
        Task<AwaitableExpectedAsReturnedTypeException> taskEx1 =
            Assert.ThrowsAsync<AwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<DateTime>(obj, methodName1, parameters1));
        AwaitableExpectedAsReturnedTypeException ex1 = await taskEx1;

        var expectedMessage1 = string.Format(AwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT, classType.GetMethod(methodName1)!.GetFullName_());
        Assert.Equal(expectedMessage1, ex1.Message);

        //---
        Task<AwaitableExpectedAsReturnedTypeException> taskEx2 =
            Assert.ThrowsAsync<AwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<DateTime>(obj, methodName2, parameters2));
        AwaitableExpectedAsReturnedTypeException ex2 = await taskEx2;

        var expectedMessage2 = string.Format(AwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT, classType.GetMethod(methodName2)!.GetFullName_());
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public async Task CallWithReturnedType_WhenReturnedTypeValueIsParentOfTheExpectedReturnedTypeValue_ShouldThrowAnAppropriateAwaitableExpectedAsReturnedTypeException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "GetSomeChildAsParent";

        object[] parameters = new object[] { };

        //--- Act & Assert ---
        Task<AppropriateAwaitableExpectedAsReturnedTypeException> taskEx =
            Assert.ThrowsAsync<AppropriateAwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<ChildClass>(obj, methodName, parameters));
        AppropriateAwaitableExpectedAsReturnedTypeException ex = await taskEx;

        var expectedMessage = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            $"<{typeof(ChildClass).GetFullName_()}>",
            typeof(Task<ParentClass>).GetFullName_()
        );
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task CallWithReturnedType_EvenWhenReturnedTypeValueIsDerivedFromTheExpectedReturnedTypeValue_ShouldThrowAnAppropriateAwaitableExpectedAsReturnedTypeException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "GetSomeChild";

        object[] parameters = new object[] {  };

        //--- Act & Assert ---
        Task<AppropriateAwaitableExpectedAsReturnedTypeException> taskEx =
            Assert.ThrowsAsync<AppropriateAwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<ParentClass>(obj, methodName, parameters));
        AppropriateAwaitableExpectedAsReturnedTypeException ex = await taskEx;

        var expectedMessage = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            $"<{typeof(ParentClass).GetFullName_()}>",
            typeof(Task<ChildClass>).GetFullName_()
        );
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task CallWithReturnedType_WhenNoMethodMatchesTheParametersType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "SomeStringMethod";

        object?[] parameters = new object?[] { null, true }; //param. valant true au lieu d'un int

        //--- Act & Assert ---
        Task<UnexistingMethodException> taskEx = Assert.ThrowsAsync<UnexistingMethodException>(() => methodCallerAsync.Call<string>(obj, methodName, parameters));
        UnexistingMethodException ex = await taskEx;
    }

    [Fact]
    public async Task CallWithReturnedType_WhenTheMethodDoesNotExistForThisType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "SomeUnexistingMethod";

        object?[] parameters = new object?[] { null, 10 };

        //--- Act & Assert ---
        Task<UnexistingMethodException> taskEx = Assert.ThrowsAsync<UnexistingMethodException>(() => methodCallerAsync.Call<string>(obj, methodName, parameters));
        UnexistingMethodException ex = await taskEx;
    }
    #endregion Non generic class

    
    #region Generic class
    [Fact]
    public async Task CallWithReturnedType_OnGenericClassWhenReturnedTypeIsValid_ShouldExecAndReturnThisValue()
    {
        //--- Arrange ---
        ISomeInterfaceAsync<bool> obj = new SomeClassAsync<bool>();
        string methodName = "SomeGenericMethod";

        object[] parameters1 = new object[] { 10, true };
        object[] parameters2 = new object[] { -10, true };

        //--- Act ---
        bool result1 = await methodCallerAsync.Call<bool>(obj, methodName, parameters1);
        bool result2 = await methodCallerAsync.Call<bool>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.Equal(parameters1[1], result1);
        Assert.False(result2);
    }

    
    [Fact]
    public async Task CallWithReturnedType_OnGenericClassWhenReturnedTypeIsNotTheExpectedOne_ShouldThrowAMismatchingReturnedTypeForThisCallException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync<string> obj = new SomeClassAsync<string>();
        string methodName = "SomeGenericMethod";

        object[] parameters = new object[] { 10, "Hello" };

        //--- Act & Assert ---
        Task<AppropriateAwaitableExpectedAsReturnedTypeException> taskEx =
            Assert.ThrowsAsync<AppropriateAwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call<bool?>(obj, methodName, parameters));
        AppropriateAwaitableExpectedAsReturnedTypeException ex = await taskEx;

        var expectedMessage = string.Format(AppropriateAwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT,
            typeof(Task).Namespace,
            $"<{typeof(bool?).GetFullName_()}>",
            typeof(Task<string?>).GetFullName_()
        ) ;
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Generic class

    #endregion Call with Returned type




    #region Call without Returned type
    #region Non generic class

    [Fact]
    public async Task CallWithoutReturnedType__TheTargetMethodShouldBeCalledWithTheCorrectParams()
    {
        //--- Arrange ---
        Mock<ISomeInterfaceAsync> mockObj = new();
        string methodName = "SomeVoidMethod";
        object[] parameters = new object[] { false };


        //--- Act ---
        await methodCallerAsync.Call(mockObj.Object, methodName, parameters);

        //--- Assert ---
        mockObj.Verify(mck => mck.SomeVoidMethod((bool)parameters[0]));
    }

    [Fact]
    public async Task CallWithoutReturnedType_WhenReturnedTypeIsAGenericAwaitable_ShouldNotCareAboutAnyReturnedValueTypeAndShouldJustCallTheMethod()
    {
        //--- Arrange ---
        Mock<ISomeInterfaceAsync> mockObj = new();
        string methodName = "SomeStringMethod"; //Returns a Task<string> , not just a Task.
        object[] parameters = new object[] { "Hi", 10 };


        //--- Act ---
        await methodCallerAsync.Call(mockObj.Object, methodName, parameters);

        //--- Assert ---
        mockObj.Verify(mck => mck.SomeStringMethod((string)parameters[0], (int)parameters[1]));
    }

    [Fact]
    public async Task CallWithoutReturnedType_WhenReturnedTypeIsNotAnAwaitable_ShouldThrowAnAwaitableExpectedAsReturnedTypeException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        Type classType = typeof(SomeClass);
        string methodName1 = "SomeVoidMethod";
        string methodName2 = "SomeStringMethod";

        object[] parameters1 = new object[] { false };
        object[] parameters2 = new object[] { "Hi", 10 };

        //--- Act & Assert ---
        Task<AwaitableExpectedAsReturnedTypeException> taskEx1 =
            Assert.ThrowsAsync<AwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call(obj, methodName1, parameters1));
        AwaitableExpectedAsReturnedTypeException ex1 = await taskEx1;

        var expectedMessage1 = string.Format(AwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT, classType.GetMethod(methodName1)!.GetFullName_());
        Assert.Equal(expectedMessage1, ex1.Message);

        //---
        Task<AwaitableExpectedAsReturnedTypeException> taskEx2 =
            Assert.ThrowsAsync<AwaitableExpectedAsReturnedTypeException>(() => methodCallerAsync.Call(obj, methodName2, parameters2));
        AwaitableExpectedAsReturnedTypeException ex2 = await taskEx2;

        var expectedMessage2 = string.Format(AwaitableExpectedAsReturnedTypeException.MESSAGE_FORMAT, classType.GetMethod(methodName2)!.GetFullName_());
        Assert.Equal(expectedMessage2, ex2.Message);
    }


    [Fact]
    public async Task CallWithoutReturnedType_WhenNoMethodMatchesTheParametersType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "SomeVoidMethod";

        object?[] parameters = new object?[] { 10 }; //param. int au lieu d'un bool

        //--- Act & Assert ---
        Task<UnexistingMethodException> taskEx = Assert.ThrowsAsync<UnexistingMethodException>(() => methodCallerAsync.Call(obj, methodName, parameters));
        UnexistingMethodException ex = await taskEx;
    }

    [Fact]
    public async Task CallWithoutReturnedType_WhenTheMethodDoesNotExistForThisType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterfaceAsync obj = new SomeClassAsync();
        string methodName = "SomeUnexistingMethod";

        object?[] parameters = new object?[] { 10 };

        //--- Act & Assert ---
        Task<UnexistingMethodException> taskEx = Assert.ThrowsAsync<UnexistingMethodException>(() => methodCallerAsync.Call(obj, methodName, parameters));
        UnexistingMethodException ex = await taskEx;
    }
    #endregion Non generic class


    #region Generic class

    [Fact]
    public async Task CallWithoutReturnedType_OnGenericClass_TheTargetMethodShouldBeCalledWithTheCorrectParams()
    {
        //--- Arrange ---
        Mock<ISomeInterfaceAsync<bool>> mockObj = new();
        string methodName1 = "SomeVoidGenericMethod";
        string methodName2 = "SomeGenericMethod"; //On peut aussi appeler une méthode retournant une valeur, mais sans se préoccuper du coup, de la valeur de retour

        object[] parameters1 = new object[] { true };
        object[] parameters2 = new object[] { 10, true };

        //--- Act & Assert ---
        await methodCallerAsync.Call(mockObj.Object, methodName1, parameters1);
        mockObj.Verify(mck => mck.SomeVoidGenericMethod((bool)parameters1[0]));
        mockObj.Verify(mck => mck.SomeGenericMethod((int)parameters2[0], (bool)parameters2[1]), Times.Never);

        await methodCallerAsync.Call(mockObj.Object, methodName2, parameters2);
        mockObj.Verify(mck => mck.SomeGenericMethod((int)parameters2[0], (bool)parameters2[1]));
    }
    #endregion Generic class

    #endregion Call without Returned type
}

//-----------------------------------------------------------------------------
#region Non generic class
public interface ISomeInterfaceAsync
{
    Task SomeVoidMethod(bool boolParam);

    Task<string> SomeStringMethod(string? s, int i);
}

class SomeClassAsync : ISomeInterfaceAsync
{
    public Task SomeVoidMethod(bool boolParam) { return Task.CompletedTask;  }

    public Task<string> SomeStringMethod(string? s, int i)
    {
        return Task.FromResult($"{s}, {i * 2}");
    }
    public async Task<string?> SomeNullableStringMethod(string s, int i)
    {
        if (i > 0) return await SomeStringMethod(s, i);
        return null;
    }

    public async Task<IReturnableAsync<string>?> GetSomeReturnable(bool returnNull)
    {
        return (returnNull) ? null : await Task.FromResult(new SomeReturnableAsync());
    }

    public async Task<ChildClass> GetSomeChild()
    {
        return await Task.FromResult(new ChildClass());
    }
    public async Task<ParentClass> GetSomeChildAsParent()
    {
        return await Task.FromResult(new ChildClass());
    }
}

public interface IReturnableAsync<T>
{
    Task<T> GetValue(T s);
}

public class SomeReturnableAsync : IReturnableAsync<string>
{
    public Task<string> GetValue(string s)
    {
        return GetDoubleStringValue(s);
    }
    private Task<string> GetDoubleStringValue(string s)
    {
        return Task.FromResult($"{s},{s}");
    }
}

class ParentClass
{

}
class ChildClass : ParentClass
{

}
#endregion Non generic class


#region Generic class
public interface ISomeInterfaceAsync<T>
{
    Task SomeVoidGenericMethod(T param);
    Task<T?> SomeGenericMethod(int i, T param);
}

class SomeClassAsync<T> : ISomeInterfaceAsync<T>
{
    public Task SomeVoidGenericMethod(T param) { return Task.CompletedTask;  }

    public Task<T?> SomeGenericMethod(int i, T param)
    {
        return Task.FromResult((i > 0) ? param : default(T?));
    }
}

#endregion Generic class