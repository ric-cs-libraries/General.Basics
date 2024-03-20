using Xunit;
using Moq;


using General.Basics.Reflection.Extensions;
using General.Basics.Reflection.DynamicCalls.Interfaces;
using General.Basics.Reflection.DynamicCalls.Abstracts;


using General.Basics.Reflection.DynamicCalls;



namespace General.Basics.Reflection.DynamicCalls.UnitTests;

public class MethodCallerTests
{
    private readonly IMethodCaller methodCaller;

    public MethodCallerTests()
    {
        methodCaller = MethodCaller.Create();
    }

    #region Call with Returned type
    #region Non generic class
    [Fact]
    public void CallWithReturnedType_WhenReturnedTypeIsValid_ShouldExecAndReturnThisValue()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        string methodName = "SomeStringMethod";

        object[] parameters1 = new object[] { "Hello", 50 };
        object?[] parameters2 = new object?[] { null, 20 };

        //--- Act ---
        var result1 = methodCaller.Call<string>(obj, methodName, parameters1);
        var result2 = methodCaller.Call<string>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.Equal($"{parameters1[0]}, {(int)parameters1[1] * 2}", result1);
        Assert.Equal($", {(int)parameters2[1]! * 2}", result2);
    }

    [Fact]
    public void CallWithReturnedType_WhenReturnedTypeIsAValidNullableString_ShouldExecAndReturnANullableString()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();  //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
        string methodName = "SomeNullableStringMethod"; //En effet l'interface ISomeInterface ne possède pas de méthode SomeNullableStringMethod

        object[] parameters1 = new object[] { "Hello", 50 };
        object?[] parameters2 = new object?[] { "Negatif Donc retournera null", -20 };

        //--- Act ---
        string? result1 = methodCaller.Call<string?>(obj, methodName, parameters1);
        string? result2 = methodCaller.Call<string?>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.Equal($"{parameters1[0]}, {(int)parameters1[1] * 2}", result1);
        Assert.Null(result2);
    }

    [Fact]
    public void CallWithReturnedType_WhenReturnedTypeIsAValidNullableInstance_ShouldExecAndReturnThisANullableInstance()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();  //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
        string methodName = "GetSomeReturnable"; //En effet l'interface ISomeInterface ne possède pas de méthode GetSomeReturnable

        object[] parameters1 = new object[] { false };
        object[] parameters2 = new object[] { true }; //la méthode retournera null

        //--- Act ---
        IReturnable<string>? result1 = methodCaller.Call<IReturnable<string>?>(obj, methodName, parameters1);
        IReturnable<string>? result2 = methodCaller.Call<IReturnable<string>?>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.NotNull(result1);
        Assert.IsType<SomeReturnable>(result1);
        Assert.IsAssignableFrom<IReturnable<string>>(result1);
        Assert.Null(result2);
    }

    [Fact]
    public void CallWithReturnedType_WhenReturnedTypeIsNotVoidButNotTheExpectedOne_ShouldThrowAMismatchingReturnedTypeForThisCallException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();  //REM.: seul le type réel(et non celui déclaratif) de obj sera déterminant, pour savoir si oui ou non il possède la méthode.
        string methodName = "GetSomeReturnable"; //En effet l'interface ISomeInterface ne possède pas de méthode GetSomeReturnable

        object[] parameters = new object[] { false };

        //--- Act & Assert ---
        var ex = Assert.Throws<MismatchingReturnedValueTypeForThisCallException>(() => methodCaller.Call<ISomeInterface>(obj, methodName, parameters));

        var expectedMessage = string.Format(MismatchingReturnedValueTypeForThisCallException.MESSAGE_FORMAT, typeof(ISomeInterface).GetFullName_(), typeof(SomeReturnable).GetFullName_());
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CallWithReturnedType_WhenMethodSignatureHasAVoidReturnType_ShouldThrowACannotExpectAnyReturnedValueFromAVoidMethodException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        string methodName = "SomeVoidMethod";

        object?[] parameters = new object?[] { true };

        //--- Act & Assert ---
        var ex = Assert.Throws<CannotExpectAnyReturnedValueFromAVoidMethodException>(() => methodCaller.Call<string>(obj, methodName, parameters));
    }

    [Fact]
    public void CallWithReturnedType_WhenNoMethodMatchesTheParametersType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        string methodName = "SomeStringMethod";

        object?[] parameters = new object?[] { null, true }; //param. valant true au lieu d'un int

        //--- Act & Assert ---
        var ex = Assert.Throws<UnexistingMethodException>(() => methodCaller.Call<string>(obj, methodName, parameters));
    }

    [Fact]
    public void CallWithReturnedType_WhenTheMethodDoesNotExistForThisType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        string methodName = "SomeUnexistingMethod";

        object?[] parameters = new object?[] { null, 10 };

        //--- Act & Assert ---
        var ex = Assert.Throws<UnexistingMethodException>(() => methodCaller.Call<string>(obj, methodName, parameters));
    }
    #endregion Non generic class


    #region Generic class
    [Fact]
    public void CallWithReturnedType_OnGenericClassWhenReturnedTypeIsValid_ShouldExecAndReturnThisValue()
    {
        //--- Arrange ---
        ISomeInterface<bool> obj = new SomeClass<bool>();
        string methodName = "SomeGenericMethod";

        object[] parameters1 = new object[] { 10, true };
        object[] parameters2 = new object[] { -10, true };

        //--- Act ---
        bool result1 = methodCaller.Call<bool>(obj, methodName, parameters1);
        bool result2 = methodCaller.Call<bool>(obj, methodName, parameters2);


        //--- Assert ---
        Assert.Equal(parameters1[1], result1);
        Assert.False(result2);
    }


    [Fact]
    public void CallWithReturnedType_OnGenericClassWhenReturnedTypeIsNotVoidButNotTheExpectedOne_ShouldThrowAMismatchingReturnedTypeForThisCallException()
    {
        //--- Arrange ---
        ISomeInterface<string> obj = new SomeClass<string>();
        string methodName = "SomeGenericMethod";

        object[] parameters = new object[] { 10, "Hello" };

        //--- Act & Assert ---
        var ex = Assert.Throws<MismatchingReturnedValueTypeForThisCallException>(() => methodCaller.Call<bool?>(obj, methodName, parameters));

        var expectedMessage = string.Format(MismatchingReturnedValueTypeForThisCallException.MESSAGE_FORMAT, typeof(bool?).GetFullName_(), typeof(string).GetFullName_());
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void CallWithReturnedType_OnGenericClassWhenMethodSignatureHasAVoidReturnType_ShouldThrowACannotExpectAnyReturnedValueFromAVoidMethodException()
    {
        //--- Arrange ---
        ISomeInterface<int> obj = new SomeClass<int>();
        string methodName = "SomeVoidGenericMethod";

        object?[] parameters = new object?[] { 10 };

        //--- Act & Assert ---
        var ex = Assert.Throws<CannotExpectAnyReturnedValueFromAVoidMethodException>(() => methodCaller.Call<string>(obj, methodName, parameters));
    }
    #endregion Generic class

    #endregion Call with Returned type


    #region Call without Returned type
    #region Non generic class
    [Fact]
    public void CallWithoutReturnedType__TheTargetMethodShouldBeCalledWithTheCorrectParams()
    {
        //--- Arrange ---
        Mock<ISomeInterface> mockObj = new();
        string methodName = "SomeVoidMethod";
        object[] parameters = new object[] { false };


        //--- Act ---
        methodCaller.Call(mockObj.Object, methodName, parameters);

        //--- Assert ---
        mockObj.Verify( mck => mck.SomeVoidMethod((bool)parameters[0]) );
    }
    
    [Fact]
    public void CallWithoutReturnedType_WhenNoMethodMatchesTheParametersType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        string methodName = "SomeVoidMethod";

        object?[] parameters = new object?[] { 10 }; //param. int au lieu d'un bool

        //--- Act & Assert ---
        var ex = Assert.Throws<UnexistingMethodException>(() => methodCaller.Call(obj, methodName, parameters));
    }
    
    [Fact]
    public void CallWithoutReturnedType_WhenTheMethodDoesNotExistForThisType_ShouldThrowAnUnexistingMethodException()
    {
        //--- Arrange ---
        ISomeInterface obj = new SomeClass();
        string methodName = "SomeUnexistingMethod";

        object?[] parameters = new object?[] {  10 };

        //--- Act & Assert ---
        var ex = Assert.Throws<UnexistingMethodException>(() => methodCaller.Call(obj, methodName, parameters));
    }
    #endregion Non generic class


    #region Generic class
    
    [Fact]
    public void CallWithoutReturnedType_OnGenericClass_TheTargetMethodShouldBeCalledWithTheCorrectParams()
    {
        //--- Arrange ---
        Mock<ISomeInterface<bool>> mockObj = new ();
        string methodName1 = "SomeVoidGenericMethod";
        string methodName2 = "SomeGenericMethod"; //On peut aussi appeler une méthode retournant une valeur, mais sans se préoccuper du coup, de la valeur de retour

        object[] parameters1 = new object[] { true };
        object[] parameters2 = new object[] { 10, true };

        //--- Act & Assert ---
        methodCaller.Call(mockObj.Object, methodName1, parameters1);
        mockObj.Verify( mck => mck.SomeVoidGenericMethod((bool)parameters1[0]) );
        mockObj.Verify( mck => mck.SomeGenericMethod((int)parameters2[0], (bool)parameters2[1]), Times.Never );

        methodCaller.Call(mockObj.Object, methodName2, parameters2);
        mockObj.Verify( mck => mck.SomeGenericMethod((int)parameters2[0], (bool)parameters2[1]) );
    }
    #endregion Generic class

    #endregion Call without Returned type
}


#region Non generic class
public interface ISomeInterface
{
    void SomeVoidMethod(bool boolParam);
    string SomeStringMethod(string? s, int i);
}

class SomeClass : ISomeInterface
{
    public void SomeVoidMethod(bool boolParam) { }

    public string SomeStringMethod(string? s, int i)
    {
        return $"{s}, {i * 2}";
    }

    public string? SomeNullableStringMethod(string s, int i)
    {
        if (i > 0) return SomeStringMethod(s, i);
        return null;
    }

    public IReturnable<string>? GetSomeReturnable(bool returnNull)
    {
        return (returnNull) ? null : new SomeReturnable();
    }
}

public interface IReturnable<T>
{
    T GetValue(T s);
}

public class SomeReturnable : IReturnable<string>
{
    public string GetValue(string s)
    {
        return GetDoubleStringValue(s);
    }
    private string GetDoubleStringValue(string s)
    {
        return $"{s},{s}";
    }
}
#endregion Non generic class


#region Generic class
public interface ISomeInterface<T>
{
    void SomeVoidGenericMethod(T param);
    T? SomeGenericMethod(int i, T param);
}

class SomeClass<T> : ISomeInterface<T>
{
    public void SomeVoidGenericMethod(T param) { }

    public T? SomeGenericMethod(int i, T param)
    {
        return (i > 0) ? param : default(T?);
    }
}

#endregion Generic class
