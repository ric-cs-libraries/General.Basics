using System.Reflection;

using Xunit;


using General.Basics.Reflection.Extensions;
using General.Basics.Reflection.POO;
using General.Basics.Reflection.DynamicCalls.Interfaces;
using General.Basics.Reflection.DynamicCalls.Abstracts;


namespace General.Basics.Reflection.DynamicCalls.UnitTests;

public class InstanciatorTests
{
    private readonly string currentAssemblyName;
    private readonly string currentNamespace;
    
    private readonly IInstanciator instanciator;

    public InstanciatorTests()
    {

        Type currentClassType = typeof(InstanciatorTests);
        currentAssemblyName = currentClassType.GetAssemblyName_();
        currentNamespace = currentClassType.Namespace!;

        instanciator = Instanciator.Create();
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

        MethodInfo[] methods = typeof(MyClassWithConstructorParamsOrNot).GetMethods();


        //--- Act ---
        object instance = instanciator.GetInstance(assemblyName, @namespace, className, constructorParams);

        //--- Assert ---
        MyClassWithConstructorParamsOrNot? exactInstance = instance as MyClassWithConstructorParamsOrNot;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClassWithConstructorParamsOrNot>(instance);
        Assert.IsAssignableFrom<IMyInterface>(instance);
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

        MethodInfo[] methods = typeof(MyClassWithConstructorParamsOrNot).GetMethods();


        //--- Act ---
        object instance = instanciator.GetInstance(assemblyName, @namespace, className);

        //--- Assert ---
        MyClassWithConstructorParamsOrNot? exactInstance = instance as MyClassWithConstructorParamsOrNot;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClassWithConstructorParamsOrNot>(instance);
        Assert.IsAssignableFrom<IMyInterface>(instance);
        Assert.IsAssignableFrom<MyClassWithConstructorParamsOrNotParent>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(MyClassWithConstructorParamsOrNot.DEFAULT_I, exactInstance!.I);
        Assert.Equal(MyClassWithConstructorParamsOrNot.DEFAULT_S, exactInstance!.S);
    }

    [Fact]
    public void GetInstance_WhenClassExistsAndHasNoConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClass";
        MethodInfo[] methods = typeof(MyClass).GetMethods();

        //--- Act ---
        object instance = instanciator.GetInstance(assemblyName, @namespace, className);

        //--- Assert ---
        MyClass? exactInstance = instance as MyClass;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClass>(instance);
        Assert.IsAssignableFrom<IMyInterface>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
    }

    [Fact]
    public void GetInstance_WhenCalledWithAClassInstanceParam_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        Class myClass = new (typeof(MyClassWithConstructorParamsOrNot));

        var constructorParam1 = 10;
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        MethodInfo[] methods = typeof(MyClassWithConstructorParamsOrNot).GetMethods();


        //--- Act ---
        object instance = instanciator.GetInstance(myClass, constructorParams);

        //--- Assert ---
        MyClassWithConstructorParamsOrNot? exactInstance = instance as MyClassWithConstructorParamsOrNot;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyClassWithConstructorParamsOrNot>(instance);
        Assert.IsAssignableFrom<IMyInterface>(instance);
        Assert.IsAssignableFrom<MyClassWithConstructorParamsOrNotParent>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(exactInstance.I, constructorParam1);
        Assert.Equal(exactInstance.S, constructorParam2);
    }

    [Fact]
    public void GetInstance_WhenClassDoesNotExist_ShouldThrowACheckIfIsNonGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassX";

        //--- Act ---
        var ex = Assert.Throws<CheckIfIsNonGenericTypeException>(() => instanciator.GetInstance(assemblyName, @namespace, className));

        var expectedMessage = string.Format(CheckIfIsNonGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{className}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenClassExistsButNoConstructorMatchTheConstructorParamsTypes_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyClassWithConstructorParamsOrNot";

        var constructorParam1 = "ShouldBeInt";
        var constructorParam2 = 10;
        var constructorParams = new object[] { constructorParam1, constructorParam2 };


        //--- Act & Assert ---
        var ex = Assert.Throws<MissingMethodException>(() => instanciator.GetInstance(assemblyName, @namespace, className, constructorParams));
    }

    [Fact]
    public void GetInstance_WhenCalledForAGenericType_ShouldThrowACheckIfIsNonGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var className = "MyGenericClass";


        //--- Act ---
        var ex = Assert.Throws<CheckIfIsNonGenericTypeException>(() => instanciator.GetInstance(assemblyName, @namespace, className));

        var expectedMessage = string.Format(CheckIfIsNonGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{className}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenCalledWithAClassInstanceParamButForAGenericType_ShouldThrowACheckIfIsNonGenericTypeException()
    {
        //--- Arrange ---
        Class myGenericClass = new(typeof(MyGenericClass<>));


        //--- Act ---
        var ex = Assert.Throws<CheckIfIsNonGenericTypeException>(() => instanciator.GetInstance(myGenericClass));

        var expectedMessage = string.Format(CheckIfIsNonGenericTypeException.MESSAGE_FORMAT, $"{myGenericClass.Type.Namespace}.{myGenericClass.Type.Name}");
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

        var typeFullName1 = $"{@namespace}.{className1}";
        var typeFullName2 = $"{@namespace}.{className2}";

        //--- Act & Assert ---
        var ex1 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciator.GetInstance(assemblyName, @namespace, className1));
        var expectedMessage1 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName1);
        Assert.Equal(expectedMessage1, ex1.Message);

        var ex2 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciator.GetInstance(assemblyName, @namespace, className2));
        var expectedMessage2 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName2);
        Assert.Equal(expectedMessage2, ex2.Message);
    }

    [Fact]
    public void GetInstance_WhenCalledWithAClassInstanceParamButForAnAbstractType_ShouldThrowAnAnAbstractTypeCannotBeInstanciatedException()
    {
        //--- Arrange ---
        Type typeOfMyAbstractClass = typeof(SomeAbstractClass);
        Class abstractClass1 = new(typeOfMyAbstractClass);
        Class abstractClass2 = new AbstractClass(typeOfMyAbstractClass);

        var typeFullName = $"{typeOfMyAbstractClass.Namespace}.{typeOfMyAbstractClass.Name}";

        //--- Act & Assert ---
        var ex1 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciator.GetInstance(abstractClass1));
        var expectedMessage1 = string.Format(AnAbstractTypeCannotBeInstanciatedException.MESSAGE_FORMAT, typeFullName);
        Assert.Equal(expectedMessage1, ex1.Message);

        var ex2 = Assert.Throws<AnAbstractTypeCannotBeInstanciatedException>(() => instanciator.GetInstance(abstractClass2));
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
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        MethodInfo[] methods = typeof(MyRecordWithConstructorParams).GetMethods();


        //--- Act ---
        object instance = instanciator.GetInstance(assemblyName, @namespace, recordName, constructorParams);

        //--- Assert ---
        MyRecordWithConstructorParams? exactInstance = instance as MyRecordWithConstructorParams;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyRecordWithConstructorParams>(instance);
        Assert.IsAssignableFrom<IMyInterface>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
        Assert.Equal(exactInstance.I, constructorParam1);
        Assert.Equal(exactInstance.S, constructorParam2);
    }

    [Fact]
    public void GetInstance_WhenRecordExistsAndHasNoConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeInstance()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecord";
        MethodInfo[] methods = typeof(MyRecord).GetMethods();

        //--- Act ---
        object instance = instanciator.GetInstance(assemblyName, @namespace, recordName);

        //--- Assert ---
        MyRecord? exactInstance = instance as MyRecord;

        Assert.NotNull(instance);
        Assert.NotNull(exactInstance);

        Assert.IsType<MyRecord>(instance);
        Assert.IsAssignableFrom<IMyInterface>(instance);
        Assert.Equal(methods, instance.GetType().GetMethods());
    }

    [Fact]
    public void GetInstance_WhenRecordDoesNotExist_ShouldThrowACheckIfIsNonGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecordX";


        //--- Act ---
        var ex = Assert.Throws<CheckIfIsNonGenericTypeException>(() => instanciator.GetInstance(assemblyName, @namespace, recordName));

        var expectedMessage = string.Format(CheckIfIsNonGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{recordName}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenRecordExistsButNoConstructorMatchTheConstructorParamsTypes_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var recordName = "MyRecordWithConstructorParams";

        var constructorParam1 = "ShouldBeInt";
        var constructorParam2 = 10;
        var constructorParams = new object[] { constructorParam1, constructorParam2 };


        //--- Act & Assert ---
        var ex = Assert.Throws<MissingMethodException>(() => instanciator.GetInstance(assemblyName, @namespace, recordName, constructorParams));
    }
    #endregion Record

    #region Struct
    [Fact]
    public void GetInstance_WhenStructExistsAndHasConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeVarStruct()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStructWithConstructorParams";

        var constructorParam1 = 10;
        var constructorParam2 = "Hello";
        var constructorParams = new object[] { constructorParam1, constructorParam2 };

        MethodInfo[] methods = typeof(MyStructWithConstructorParams).GetMethods();


        //--- Act ---
        object varStruct = instanciator.GetInstance(assemblyName, @namespace, structName, constructorParams);
        MyStructWithConstructorParams exactVarStruct = (MyStructWithConstructorParams)varStruct!;

        //--- Assert ---
        Assert.NotNull(varStruct);

        Assert.IsType<MyStructWithConstructorParams>(varStruct);
        Assert.IsAssignableFrom<IMyInterface>(varStruct);
        Assert.Equal(methods, varStruct.GetType().GetMethods());
        Assert.Equal(exactVarStruct.I, constructorParam1);
        Assert.Equal(exactVarStruct.S, constructorParam2);
    }

    [Fact]
    public void GetInstance_WhenStructExistsAndHasNoConstructorParams_ShouldReturnTheCorrectlyInializedGoodTypeVarStruct()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStruct";
        MethodInfo[] methods = typeof(MyStruct).GetMethods();

        //--- Act ---
        object varStruct = instanciator.GetInstance(assemblyName, @namespace, structName);

        //--- Assert ---
        Assert.NotNull(varStruct);

        Assert.IsType<MyStruct>(varStruct);
        Assert.IsAssignableFrom<IMyInterface>(varStruct);
        Assert.Equal(methods, varStruct.GetType().GetMethods());
    }

    [Fact]
    public void GetInstance_WhenStructDoesNotExist_ShouldThrowACheckIfIsNonGenericTypeException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStructX";


        //--- Act ---
        var ex = Assert.Throws<CheckIfIsNonGenericTypeException>(() => instanciator.GetInstance(assemblyName, @namespace, structName));

        var expectedMessage = string.Format(CheckIfIsNonGenericTypeException.MESSAGE_FORMAT, $"{@namespace}.{structName}");
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void GetInstance_WhenStructExistsButNoConstructorMatchTheConstructorParamsTypes_ShouldThrowAMissingMethodException()
    {
        //--- Arrange ---
        var assemblyName = currentAssemblyName;
        var @namespace = currentNamespace;
        var structName = "MyStructWithConstructorParams";

        var constructorParam1 = "ShouldBeInt";
        var constructorParam2 = 10;
        var constructorParams = new object[] { constructorParam1, constructorParam2 };


        //--- Act & Assert ---
        var ex = Assert.Throws<MissingMethodException>(() => instanciator.GetInstance(assemblyName, @namespace, structName, constructorParams));
    }
    #endregion Struct
}

public class MyGenericClass<T>
{

}

public interface IMyInterface
{
    string GetInfos();
}

public class MyClassWithConstructorParamsOrNotParent
{
    public const int DEFAULT_I = 1000;
    public int I { get; } = DEFAULT_I;

    public MyClassWithConstructorParamsOrNotParent(int i)
    {
        I = i;
    }
    public MyClassWithConstructorParamsOrNotParent()
    {
    }
}

    public class MyClassWithConstructorParamsOrNot : MyClassWithConstructorParamsOrNotParent, IMyInterface
{
    public const string DEFAULT_S = "I'm the default value";
    public string S { get; } = DEFAULT_S;


    public MyClassWithConstructorParamsOrNot(int i, string s) : base(i)
    {
        S = s;
    }

    public MyClassWithConstructorParamsOrNot() : base()
    {

    }

    public string GetInfos()
    {
        return "";
    }
}

class MyClass : IMyInterface
{
    public string GetInfos()
    {
        return "";
    }
}


public record MyRecordWithConstructorParams(int I, string S) : IMyInterface
{
    public string GetInfos()
    {
        return "";
    }
}


record MyRecord : IMyInterface
{
    public string GetInfos()
    {
        return "";
    }
}


public struct MyStructWithConstructorParams : IMyInterface
{
    public int I { get; }
    public string S { get; }

    public MyStructWithConstructorParams(int i, string s)
    {
        I = i;
        S = s;
    }
    public string GetInfos()
    {
        return "";
    }
}
struct MyStruct : IMyInterface
{
    public string GetInfos()
    {
        return "";
    }
}


abstract class SomeAbstractClass
{

}
