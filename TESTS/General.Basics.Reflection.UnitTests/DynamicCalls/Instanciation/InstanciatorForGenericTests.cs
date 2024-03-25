using System.Reflection;

using Xunit;


using General.Basics.Reflection.Extensions;
using General.Basics.Reflection.POO;
using General.Basics.Reflection.DynamicCalls.Interfaces;
using General.Basics.Reflection.DynamicCalls.Abstracts;

using General.Basics.Reflection.DynamicCalls;


namespace General.Basics.Reflection.DynamicCalls.UnitTests;

public class InstanciatorForGenericTests
{
    private readonly string currentAssemblyName;
    private readonly string currentNamespace;

    private readonly IInstanciatorForGeneric instanciatorForGeneric;

    public InstanciatorForGenericTests()
    {

        Type currentClassType = typeof(InstanciatorForGenericTests);
        currentAssemblyName = currentClassType.GetAssemblyName_();
        currentNamespace = currentClassType.Namespace!;

        instanciatorForGeneric = InstanciatorForGeneric.Create();
    }

    #region Class
    [Fact]
    public void GetInstance_WhenClassExistsAndHasConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassWithConstructorParamsOrNot";

        var constructorParam1 = 10;
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        var genericParametersType = new Type[] { typeof(string) };

        MethodInfo[] methods = typeof(MyClassWithConstructorParamsOrNot<string>).GetMethods();


        //--- Act ---
        object instance = instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType, constructorParams);

        //--- Assert ---
        MyClassWithConstructorParamsOrNot<string>? exactInstance = instance as MyClassWithConstructorParamsOrNot<string>;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClassWithConstructorParamsOrNot<string>>(instance);
        Assert.IsAssignableFrom<IMyInterface<string>>(instance);
        Assert.IsAssignableFrom<MyClassWithConstructorParamsOrNotParent>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(exactInstance.I, constructorParam1);
        Assert.Equal(exactInstance.S, constructorParam2);
    }
    
    [Fact]
    public void GetInstance_WhenClassExistsAndHasManyConstructors_ShouldUseTheGoodConstructorAndReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassWithConstructorParamsOrNot";

        var genericParametersType = new Type[] { typeof(bool) };

        MethodInfo[] methods = typeof(MyClassWithConstructorParamsOrNot<bool>).GetMethods();


        //--- Act ---
        object instance = instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType);

        //--- Assert ---
        MyClassWithConstructorParamsOrNot<bool>? exactInstance = instance as MyClassWithConstructorParamsOrNot<bool>;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClassWithConstructorParamsOrNot<bool>>(instance);
        Assert.IsAssignableFrom<IMyInterface<bool>>(instance);
        Assert.IsAssignableFrom<MyClassWithConstructorParamsOrNotParent>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(MyClassWithConstructorParamsOrNot<bool>.DEFAULT_I, exactInstance!.I);
        Assert.Equal(MyClassWithConstructorParamsOrNot<bool>.DEFAULT_S, exactInstance!.S);
    }
    
    [Fact]
    public void GetInstance_WhenClassExistsAndHasNoConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClass2";
        
        var genericParametersType = new Type[] { typeof(MyRecord) };

        MethodInfo[] methods = typeof(MyClass2<MyRecord>).GetMethods();

        //--- Act ---
        object instance = instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType);

        //--- Assert ---
        MyClass2<MyRecord>? exactInstance = instance as MyClass2<MyRecord>;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClass2<MyRecord>>(instance);
        Assert.IsAssignableFrom<IMyInterface<string?>>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
    }


    [Fact]
    public void GetInstance_WhenCalledWithAClassInstanceParam_ShouldReturnTheCorrectlyInitializedGoodTypeInstance()
    {
        //--- Arrange ---
        Class myGenericClass = new(typeof(MyClassWithConstructorParamsOrNot<>));

        var constructorParam1 = 10;
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        var genericParametersType = new Type[] { typeof(string) };

        MethodInfo[] methods = typeof(MyClassWithConstructorParamsOrNot<string>).GetMethods();


        //--- Act ---
        object instance = instanciatorForGeneric.GetInstance(myGenericClass, genericParametersType, constructorParams);

        //--- Assert ---
        MyClassWithConstructorParamsOrNot<string>? exactInstance = instance as MyClassWithConstructorParamsOrNot<string>;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClassWithConstructorParamsOrNot<string>>(instance);
        Assert.IsAssignableFrom<IMyInterface<string>>(instance);
        Assert.IsAssignableFrom<MyClassWithConstructorParamsOrNotParent>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(exactInstance.I, constructorParam1);
        Assert.Equal(exactInstance.S, constructorParam2);
    }


    [Fact]
    public void GetInstance_WhenClassDoesNotExist_ShouldThrowACheckIfIsGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassX";

        var genericParametersType = new Type[] { typeof(int) };

        //--- Act ---
        var ex = Assert.Throws<CheckIfIsGenericTypeException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType));

        var expectedMessage = string.Format(CheckIfIsGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{className}");
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Fact]
    public void GetInstance_WhenClassExistsButNoConstructorMatchTheConstructorParamsTypes_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassWithConstructorParamsOrNot";

        var genericParametersType = new Type[] { typeof(bool) };

        var constructorParam1 = 10;
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };


        //--- Act & Assert ---
        var ex = Assert.Throws<MissingMethodException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType, constructorParams));
    }

    [Fact]
    public void GetInstance_WhenForClassGenericParametersTypeAreMissing_ShouldThrowAMissingGenericParametersTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClass2";

        var genericParametersType = new Type[] {  };

        var methodName = $"{nameof(Type_)}.{nameof(Type_.GetGenericTypeFromNamesInfos)}";

        //--- Act & Assert ---
        var ex = Assert.Throws<MissingGenericParametersTypeException>(() =>
            instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType));

        var expectedMessage = string.Format(MissingGenericParametersTypeException.MESSAGE_FORMAT, methodName, $"{@namespace}.{className}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenForClassGenericParameterTypeDoesNotComplyToSomeConstraint_ShouldThrowAGenericParameterTypeViolatingSomeConstraintException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassWithConstraints";

        var genericParametersType = new Type[] { typeof(MyClassWithNoNoParamConstructor) };


        //--- Act & Assert ---
        var ex = Assert.Throws<GenericParameterTypeViolatingSomeConstraintException>(() =>
            instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType));

        var expectedMessage = string.Format(GenericParameterTypeViolatingSomeConstraintException.MESSAGE_FORMAT,
            $"GenericArguments[0], '{@namespace}.{genericParametersType[0].Name}', on '{@namespace}.{className}`1[T]' violates the constraint of type 'T'.");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenCalledForANonGenericType_ShouldThrowACheckIfIsGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyNonGenericClass";

        var genericParametersType = new Type[] { typeof(bool) };

        //--- Act ---
        var ex = Assert.Throws<CheckIfIsGenericTypeException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, className, genericParametersType));

        var expectedMessage = string.Format(CheckIfIsGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{className}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenCalledWithAClassInstanceParamButForANonGenericType_ShouldThrowACheckIfIsGenericTypeException()
    {
        //--- Arrange ---
        Class myClass = new Class(typeof(MyNonGenericClass));

        var genericParametersType = new Type[] { typeof(bool) };

        //--- Act ---
        var ex = Assert.Throws<CheckIfIsGenericTypeException>(() => instanciatorForGeneric.GetInstance(myClass, genericParametersType));

        var expectedMessage = string.Format(CheckIfIsGenericTypeException.MESSAGE_FORMAT, $"{myClass.Type.Namespace}.{myClass.Type.Name}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenCalledForAnAbstractType_ShouldThrowAnAnAbstractTypeCannotBeInstanciatedException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;

        var className1 = "SomeAbstractClass";
        var className2 = "IMyInterface";

        var genericParametersType = new Type[] { typeof(bool) };

        var typeFullName1 = $"{@namespace}.{className1}{Type_.GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParametersType.Length}";
        var typeFullName2 = $"{@namespace}.{className2}{Type_.GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParametersType.Length}";

        //--- Act & Assert ---
        var ex1 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, className1, genericParametersType));
        var expectedMessage1 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName1);
        Assert.Equal(expectedMessage1, ex1.Message);

        var ex2 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, className2, genericParametersType));
        var expectedMessage2 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName2);
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public void GetInstance_WhenCalledWithAClassInstanceParamButForAnAbstractType_ShouldThrowAnAnAbstractTypeCannotBeInstanciatedException()
    {
        //--- Arrange ---
        Type typeOfMyAbstractClass = typeof(SomeAbstractClass<>);
        Class abstractClass1 = new(typeOfMyAbstractClass);
        Class abstractClass2 = new AbstractClass(typeOfMyAbstractClass);

        var genericParametersType = new Type[] { typeof(bool) };

        var typeFullName = $"{typeOfMyAbstractClass.Namespace}.{typeOfMyAbstractClass.Name}";

        //--- Act & Assert ---
        var ex1 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciatorForGeneric.GetInstance(abstractClass1, genericParametersType));
        var expectedMessage1 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName);
        Assert.Equal(expectedMessage1, ex1.Message);

        var ex2 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciatorForGeneric.GetInstance(abstractClass2, genericParametersType));
        var expectedMessage2 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName);
        Assert.Equal(expectedMessage2, ex2.Message);
    }
    #endregion Class

    #region Record
    [Fact]
    public void GetInstance_WhenRecordExistsAndHasConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecordWithConstructorParams";

        var constructorParam1 = 10;
        var constructorParam2 = true;
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        var genericParametersType = new Type[] { typeof(bool) };

        MethodInfo[] methods = typeof(MyRecordWithConstructorParams<bool>).GetMethods();


        //--- Act ---
        object instance = instanciatorForGeneric.GetInstance(assemblyName, @namespace, recordName, genericParametersType, constructorParams);

        //--- Assert ---
        MyRecordWithConstructorParams<bool>? exactInstance = instance as MyRecordWithConstructorParams<bool>;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyRecordWithConstructorParams<bool>>(instance);
        Assert.IsAssignableFrom<IMyInterface<bool>>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(exactInstance.I, constructorParam1);
        Assert.Equal(exactInstance.S, constructorParam2);
        //Assert.False(exactInstance.GetInfos());
    }
    
    
    [Fact]
    public void GetInstance_WhenRecordExistsAndHasNoConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecord2";

        var genericParametersType = new Type[] { typeof(bool) };

        MethodInfo[] methods = typeof(MyRecord2<bool>).GetMethods();

        //--- Act ---
        object instance = instanciatorForGeneric.GetInstance(assemblyName, @namespace, recordName, genericParametersType);

        //--- Assert ---
        MyRecord2<bool>? exactInstance = instance as MyRecord2<bool>;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyRecord2<bool>>(instance);
        Assert.IsAssignableFrom<IMyInterface<int>>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
    }
    
    [Fact]
    public void GetInstance_WhenRecordDoesNotExist_ShouldThrowACheckIfIsGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecordX";

        var genericParametersType = new Type[] { typeof(int) };


        //--- Act ---
        var ex = Assert.Throws<CheckIfIsGenericTypeException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, recordName, genericParametersType));

        var expectedMessage = string.Format(CheckIfIsGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{recordName}");
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Fact]
    public void GetInstance_WhenRecordExistsButNoConstructorMatchTheConstructorParamsTypes_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecordWithConstructorParams";

        var genericParametersType = new Type[] { typeof(bool) };

        var constructorParam1 = 10;
        var constructorParam2 = "ShouldBeBool";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };


        //--- Act & Assert ---
        var ex = Assert.Throws<MissingMethodException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, recordName, genericParametersType, constructorParams));
    }
    
    [Fact]
    public void GetInstance_WhenForRecordGenericParametersTypeAreMissing_ShouldThrowAMissingGenericParametersTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecord2";

        var genericParametersType = new Type[] {  };

        var methodName = $"{nameof(Type_)}.{nameof(Type_.GetGenericTypeFromNamesInfos)}";

        //--- Act & Assert ---
        var ex = Assert.Throws<MissingGenericParametersTypeException>(() =>
            instanciatorForGeneric.GetInstance(assemblyName, @namespace, recordName, genericParametersType));

        var expectedMessage = string.Format(MissingGenericParametersTypeException.MESSAGE_FORMAT, methodName, $"{@namespace}.{recordName}");
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Record

    #region Struct
    [Fact]
    public void GetInstance_WhenStructExistsAndHasConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeVarStruct()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStructWithConstructorParams2";

        var constructorParam1 = 10;
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        var genericParametersType = new Type[] { typeof(string) };

        MethodInfo[] methods = typeof(MyStructWithConstructorParams2<string>).GetMethods();


        //--- Act ---
        object varStruct = instanciatorForGeneric.GetInstance(assemblyName, @namespace, structName, genericParametersType, constructorParams);

        //--- Assert ---
        MyStructWithConstructorParams2<string> exactVarStruct = (MyStructWithConstructorParams2<string>)varStruct!;

        Assert.NotNull(varStruct);

        Assert.IsType<MyStructWithConstructorParams2<string>>(varStruct);
        Assert.IsAssignableFrom<IMyInterface<bool>>(varStruct);
        Assert.Equal(methods, varStruct.GetType().GetMethods());
        Assert.Equal(exactVarStruct.I, constructorParam1);
        Assert.Equal(exactVarStruct.S, constructorParam2);
        //Assert.True(exactVarStruct.GetInfos());
    }

    
    [Fact]
    public void GetInstance_WhenStructExistsAndHasNoConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeVarStruct()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStruct2";

        var genericParametersType = new Type[] { typeof(bool) };

        MethodInfo[] methods = typeof(MyStruct2<bool>).GetMethods();

        //--- Act ---
        object varStruct = instanciatorForGeneric.GetInstance(assemblyName, @namespace, structName, genericParametersType);

        //--- Assert ---
        MyStruct2<bool> exactVarStruct = (MyStruct2<bool>)varStruct!;

        Assert.NotNull(varStruct);

        Assert.IsType<MyStruct2<bool>>(varStruct);
        Assert.IsAssignableFrom<IMyInterface<char>>(varStruct);
        Assert.Equal(methods, varStruct.GetType().GetMethods());
    }
    
    [Fact]
    public void GetInstance_WhenStructDoesNotExist_ShouldThrowACheckIfIsGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStructX";

        var genericParametersType = new Type[] { typeof(int) };


        //--- Act ---
        var ex = Assert.Throws<CheckIfIsGenericTypeException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, structName, genericParametersType));

        var expectedMessage = string.Format(CheckIfIsGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{structName}");
        Assert.Equal(expectedMessage, ex.Message);
    }
    
    [Fact]
    public void GetInstance_WhenStructExistsButNoConstructorMatchTheConstructorParamsTypes_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStructWithConstructorParams2";

        var genericParametersType = new Type[] { typeof(bool) };

        var constructorParam1 = 10;
        var constructorParam2 = "ShouldBeBool";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };


        //--- Act & Assert ---
        var ex = Assert.Throws<MissingMethodException>(() => instanciatorForGeneric.GetInstance(assemblyName, @namespace, structName, genericParametersType, constructorParams));
    }
    
    [Fact]
    public void GetInstance_WhenForStructGenericParametersTypeAreMissing_ShouldThrowAMissingGenericParametersTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStruct2";

        var genericParametersType = new Type[] { };

        var methodName = $"{nameof(Type_)}.{nameof(Type_.GetGenericTypeFromNamesInfos)}";

        //--- Act & Assert ---
        var ex = Assert.Throws<MissingGenericParametersTypeException>(() =>
            instanciatorForGeneric.GetInstance(assemblyName, @namespace, structName, genericParametersType));

        var expectedMessage = string.Format(MissingGenericParametersTypeException.MESSAGE_FORMAT, methodName, $"{@namespace}.{structName}");
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Struct
}

public class MyNonGenericClass
{
}

public interface IMyInterface<T>
{
    T? GetInfos();
}


public class MyClassWithConstructorParamsOrNot<T> : MyClassWithConstructorParamsOrNotParent, IMyInterface<T>
{
    public static readonly T? DEFAULT_S = default(T);

    public T? S { get; } = DEFAULT_S;
    
    public MyClassWithConstructorParamsOrNot(int i, T s) : base(i)
    {
        S = s;
    }

    public MyClassWithConstructorParamsOrNot()
    {

    }

    public T? GetInfos()
    {
        return default(T);
    }
}

class MyClass2<U> : IMyInterface<string?>
{
    public string? GetInfos()
    {
        return "";
    }
    private void F(U u)
    {
    }
}

class MyClassWithConstraints<T>
    where T: class, new()
{

}
class MyClassWithNoNoParamConstructor
{
    public MyClassWithNoNoParamConstructor(int param)
    {

    }
}


public record MyRecordWithConstructorParams<T>(int I, T S) : IMyInterface<T>
{
    public T? GetInfos()
    {
        return default(T);
    }
}


record MyRecord2<U> : IMyInterface<int>
{
    public int GetInfos()
    {
        return 5;
    }
    private void F(U u)
    {
    }
}


public struct MyStructWithConstructorParams2<T> : IMyInterface<bool>
{
    public int I { get; }
    public T? S { get; }

    public MyStructWithConstructorParams2(int i, T s)
    {
        I = i;
        S = s;
    }
    public bool GetInfos()
    {
        return true;
    }
}
struct MyStruct2<U> : IMyInterface<char>
{
    public char GetInfos()
    {
        return 'a';
    }
    private void F(U u)
    {
    }
}

abstract class SomeAbstractClass<T>
{

}