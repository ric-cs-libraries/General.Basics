using Xunit;

using General.Basics.Reflection.Extensions;

using static General.Basics.Reflection.Extensions.Type_;


namespace General.Basics.Reflection.Extensions.UnitTests;

public class Type_Tests
{
    readonly string currentNamespace;
    readonly string currentAssemblyName;

    public Type_Tests()
    {
        Type currentClassType = typeof(Type_Tests);
        currentAssemblyName = currentClassType.GetAssemblyName_();
        currentNamespace = currentClassType.Namespace!;
    }

    #region GetTypeFromNamesInfos
    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeExistsAsConcreteClass_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string classAssemblyName = "General.Basics";
        string classNamespace = "General.Basics.Generators";
        string className = "IdsGenerator";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(classAssemblyName, classNamespace, className);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var partialAssemblyQualifiedName = type!.AssemblyQualifiedName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{classAssemblyName}, Version";
        var expectedClassFullName = $"{classNamespace}.{className}";

        Assert.NotNull(type);
        Assert.Equal(className, type!.Name);
        Assert.Equal(classNamespace, type!.Namespace);
        Assert.Equal(expectedClassFullName, type!.FullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{expectedClassFullName}, {expectedPartialAssemblyFullName}", partialAssemblyQualifiedName);
        Assert.Equal($"{classAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.False(type.IsGenericType);
        Assert.Empty(type.GetGenericArguments());
    }

    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeExistsAsConcreteClassWithExplicitParentClass_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string classAssemblyName = currentAssemblyName;
        string classNamespace = currentNamespace;
        string className = "MyChildClass";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(classAssemblyName, classNamespace, className);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var partialAssemblyQualifiedName = type!.AssemblyQualifiedName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{classAssemblyName}, Version";
        var expectedClassFullName = $"{classNamespace}.{className}";

        Assert.NotNull(type);
        Assert.Equal(className, type!.Name);
        Assert.Equal(classNamespace, type!.Namespace);
        Assert.Equal(expectedClassFullName, type!.FullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{expectedClassFullName}, {expectedPartialAssemblyFullName}", partialAssemblyQualifiedName);
        Assert.Equal($"{classAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.Equal(typeof(MyParentClass), type.BaseType); //<<<
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.False(type.IsGenericType);
        Assert.Empty(type.GetGenericArguments());
    }

    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeExistsAsInterface_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string interfaceAssemblyName = currentAssemblyName;
        string interfaceNamespace = currentNamespace;
        string interfaceName = "IMyInterface1";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(interfaceAssemblyName, interfaceNamespace, interfaceName);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var partialAssemblyQualifiedName = type!.AssemblyQualifiedName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{interfaceAssemblyName}, Version";
        var expectedinterfaceFullName = $"{interfaceNamespace}.{interfaceName}";

        Assert.NotNull(type);
        Assert.Equal(interfaceName, type!.Name);
        Assert.Equal(interfaceNamespace, type!.Namespace);
        Assert.Equal(expectedinterfaceFullName, type!.FullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{expectedinterfaceFullName}, {expectedPartialAssemblyFullName}", partialAssemblyQualifiedName);
        Assert.Equal($"{interfaceAssemblyName}.dll", type.Module.Name);
        Assert.False(type.IsClass);
        Assert.True(type.IsInterface);
        Assert.True(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.False(type.IsGenericType);
        Assert.Empty(type.GetGenericArguments());
    }

    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeExistsAsAbstractClass_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string abstractClassAssemblyName = currentAssemblyName;
        string abstractClassNamespace = currentNamespace;
        string abstractClassName = "MyAbstractClass";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(abstractClassAssemblyName, abstractClassNamespace, abstractClassName);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var partialAssemblyQualifiedName = type!.AssemblyQualifiedName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{abstractClassAssemblyName}, Version";
        var expectedabstractClassFullName = $"{abstractClassNamespace}.{abstractClassName}";

        Assert.NotNull(type);
        Assert.Equal(abstractClassName, type!.Name);
        Assert.Equal(abstractClassNamespace, type!.Namespace);
        Assert.Equal(expectedabstractClassFullName, type!.FullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{expectedabstractClassFullName}, {expectedPartialAssemblyFullName}", partialAssemblyQualifiedName);
        Assert.Equal($"{abstractClassAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.True(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.False(type.IsGenericType);
        Assert.Empty(type.GetGenericArguments());
    }

    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeExistsAsARecord_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string recordAssemblyName = currentAssemblyName;
        string recordNamespace = currentNamespace;
        string recordName = "MyRecord";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(recordAssemblyName, recordNamespace, recordName);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var partialAssemblyQualifiedName = type!.AssemblyQualifiedName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{recordAssemblyName}, Version";
        var expectedrecordFullName = $"{recordNamespace}.{recordName}";

        Assert.NotNull(type);
        Assert.Equal(recordName, type!.Name);
        Assert.Equal(recordNamespace, type!.Namespace);
        Assert.Equal(expectedrecordFullName, type!.FullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{expectedrecordFullName}, {expectedPartialAssemblyFullName}", partialAssemblyQualifiedName);
        Assert.Equal($"{recordAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.False(type.IsGenericType);
        Assert.Empty(type.GetGenericArguments());
    }

    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeExistsAsAStruct_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string structAssemblyName = currentAssemblyName;
        string structNamespace = currentNamespace;
        string structName = "MyStruct";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(structAssemblyName, structNamespace, structName);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var partialAssemblyQualifiedName = type!.AssemblyQualifiedName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{structAssemblyName}, Version";
        var expectedstructFullName = $"{structNamespace}.{structName}";

        Assert.NotNull(type);
        Assert.Equal(structName, type!.Name);
        Assert.Equal(structNamespace, type!.Namespace);
        Assert.Equal(expectedstructFullName, type!.FullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{expectedstructFullName}, {expectedPartialAssemblyFullName}", partialAssemblyQualifiedName);
        Assert.Equal($"{structAssemblyName}.dll", type.Module.Name);
        Assert.False(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.True(type.IsValueType);
        Assert.False(type.IsGenericType);
        Assert.Empty(type.GetGenericArguments());
    }

    [Fact]
    public void GetTypeFromNamesInfos_WhenTypeIsGenericType_ShouldReturnNull()
    {
        //--- Arrange ---
        string genericClassAssemblyName = currentAssemblyName;
        string genericClassNamespace = currentNamespace;
        string genericClassName = "MyGenericClass";

        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(genericClassAssemblyName, genericClassNamespace, genericClassName);

        //--- Assert ---
        Assert.Null(type);
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData("w", "x", "y")]
    [InlineData("General.Basics", "x", "y")]
    [InlineData("General.Basics", "General.Basics.Generators", "y")]
    [InlineData("w", "General.Basics.Generators", "y")]
    [InlineData("w", "General.Basics.Generators", "IdsGenerator")]
    [InlineData("w", "x", "IdsGenerator")]
    [InlineData("General.Basics", "x", "IdsGenerator")]
    public void GetTypeFromNamesInfos_WhenTypeDoesNotExist_ShouldReturnNull(string maybeUnexistingAssemblyName, string maybeUnexistingNameSpace, string maybeUnexistingClassName)
    {
        //--- Act ---
        Type? type = Type_.GetTypeFromNamesInfos(maybeUnexistingAssemblyName, maybeUnexistingNameSpace, maybeUnexistingClassName);

        //--- Assert ---
        Assert.Null(type);
    }
    #endregion GetTypeFromNamesInfos



    #region GetGenericTypeFromNamesInfos
    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeExistsAsConcreteClass_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string genericClassAssemblyName = currentAssemblyName;
        string genericClassNamespace = currentNamespace;
        string genericClassName = "MyGenericClass";
        Type[] genericParamsType = new Type[] { typeof(int), typeof(string) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericClassAssemblyName, genericClassNamespace, genericClassName, genericParamsType);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{genericClassAssemblyName}, Version";
        var expectedGenericClassName = $"{genericClassName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParamsType.Length}";

        Assert.NotNull(type);
        Assert.Equal(expectedGenericClassName, type!.Name);
        Assert.Equal(genericClassNamespace, type!.Namespace);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{genericClassAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.True(type.IsGenericType);
        Assert.Equal(genericParamsType.Length, type.GetGenericArguments().Length);
        Assert.Equal(genericParamsType[0], type.GetGenericArguments()[0]);
        Assert.Equal(genericParamsType[1], type.GetGenericArguments()[1]);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeExistsAsConcreteClassWithExplicitParentClass_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string genericClassAssemblyName = currentAssemblyName;
        string genericClassNamespace = currentNamespace;
        string genericClassName = "MyGenericChildClass";
        Type[] genericParamsType = new Type[] { typeof(int), typeof(string), typeof(bool) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericClassAssemblyName, genericClassNamespace, genericClassName, genericParamsType);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{genericClassAssemblyName}, Version";
        var expectedGenericClassName = $"{genericClassName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParamsType.Length}";

        Assert.NotNull(type);
        Assert.Equal(expectedGenericClassName, type!.Name);
        Assert.Equal(genericClassNamespace, type!.Namespace);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{genericClassAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.Equal(typeof(MyGenericParentClass<int, string, bool>), type.BaseType);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.True(type.IsGenericType);
        Assert.Equal(genericParamsType.Length, type.GetGenericArguments().Length);
        Assert.Equal(genericParamsType[0], type.GetGenericArguments()[0]);
        Assert.Equal(genericParamsType[1], type.GetGenericArguments()[1]);
        Assert.Equal(genericParamsType[2], type.GetGenericArguments()[2]);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeExistsAsInterface_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string genericInterfaceAssemblyName = currentAssemblyName;
        string genericInterfaceNamespace = currentNamespace;
        string genericInterfaceName = "IMyGenericInterface";
        Type[] genericParamsType = new Type[] { typeof(int) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericInterfaceAssemblyName, genericInterfaceNamespace, genericInterfaceName, genericParamsType);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{genericInterfaceAssemblyName}, Version";
        var expectedGenericInterfaceName = $"{genericInterfaceName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParamsType.Length}";

        Assert.NotNull(type);
        Assert.Equal(expectedGenericInterfaceName, type!.Name);
        Assert.Equal(genericInterfaceNamespace, type!.Namespace);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{genericInterfaceAssemblyName}.dll", type.Module.Name);
        Assert.False(type.IsClass);
        Assert.True(type.IsInterface);
        Assert.True(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.True(type.IsGenericType);
        Assert.Equal(genericParamsType.Length, type.GetGenericArguments().Length);
        Assert.Equal(genericParamsType[0], type.GetGenericArguments()[0]);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeExistsAsAbstractClass_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string genericAbstractClassAssemblyName = currentAssemblyName;
        string genericAbstractClassNamespace = currentNamespace;
        string genericAbstractClassName = "MyGenericAbstractClass";
        Type[] genericParamsType = new Type[] { typeof(MyRecord), typeof(int) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericAbstractClassAssemblyName, genericAbstractClassNamespace, genericAbstractClassName, genericParamsType);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{genericAbstractClassAssemblyName}, Version";
        var expectedGenericAbstractClassName = $"{genericAbstractClassName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParamsType.Length}";

        Assert.NotNull(type);
        Assert.Equal(expectedGenericAbstractClassName, type!.Name);
        Assert.Equal(genericAbstractClassNamespace, type!.Namespace);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{genericAbstractClassAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.True(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.True(type.IsGenericType);
        Assert.Equal(genericParamsType.Length, type.GetGenericArguments().Length);
        Assert.Equal(genericParamsType[0], type.GetGenericArguments()[0]);
        Assert.Equal(genericParamsType[1], type.GetGenericArguments()[1]);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeExistsAsARecord_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string genericRecordAssemblyName = currentAssemblyName;
        string genericRecordNamespace = currentNamespace;
        string genericRecordName = "MyGenericRecord";
        Type[] genericParamsType = new Type[] { typeof(IMyInterface1), typeof(int) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericRecordAssemblyName, genericRecordNamespace, genericRecordName, genericParamsType);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{genericRecordAssemblyName}, Version";
        var expectedGenericRecordName = $"{genericRecordName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParamsType.Length}";

        Assert.NotNull(type);
        Assert.Equal(expectedGenericRecordName, type!.Name);
        Assert.Equal(genericRecordNamespace, type!.Namespace);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{genericRecordAssemblyName}.dll", type.Module.Name);
        Assert.True(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.False(type.IsValueType);
        Assert.True(type.IsGenericType);
        Assert.Equal(genericParamsType.Length, type.GetGenericArguments().Length);
        Assert.Equal(genericParamsType[0], type.GetGenericArguments()[0]);
        Assert.Equal(genericParamsType[1], type.GetGenericArguments()[1]);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeExistsAsAStruct_ShouldReturnACorrectlyInitializedType()
    {
        //--- Arrange ---
        string genericStructAssemblyName = currentAssemblyName;
        string genericStructNamespace = currentNamespace;
        string genericStructName = "MyGenericStruct";
        Type[] genericParamsType = new Type[] { typeof(IMyInterface1), typeof(IMyGenericInterface<MyRecord>) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(genericStructAssemblyName, genericStructNamespace, genericStructName, genericParamsType);

        //--- Assert ---
        var partialAssemblyFullName = type!.Assembly.FullName!.Split("=")[0];
        var expectedPartialAssemblyFullName = $"{genericStructAssemblyName}, Version";
        var expectedGenericStructName = $"{genericStructName}{GENERIC_TYPES_NAME_SEPARATOR_SYMBOL}{genericParamsType.Length}";

        Assert.NotNull(type);
        Assert.Equal(expectedGenericStructName, type!.Name);
        Assert.Equal(genericStructNamespace, type!.Namespace);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal(expectedPartialAssemblyFullName, partialAssemblyFullName);
        Assert.Equal($"{genericStructAssemblyName}.dll", type.Module.Name);
        Assert.False(type.IsClass);
        Assert.False(type.IsInterface);
        Assert.False(type.IsAbstract);
        Assert.True(type.IsValueType);
        Assert.True(type.IsGenericType);
        Assert.Equal(genericParamsType.Length, type.GetGenericArguments().Length);
        Assert.Equal(genericParamsType[0], type.GetGenericArguments()[0]);
        Assert.Equal(genericParamsType[1], type.GetGenericArguments()[1]);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenTypeIsNotGenericType_ShouldReturnNull()
    {
        //--- Arrange ---
        string nonGenericClassAssemblyName = currentAssemblyName;
        string nonGenericClassNamespace = currentNamespace;
        string nonGenericClassName = "MyParentClass";
        Type[] genericParamsType = new Type[] { typeof(int), typeof(int), typeof(bool) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(nonGenericClassAssemblyName, nonGenericClassNamespace, nonGenericClassName, genericParamsType);

        //--- Assert ---
        Assert.Null(type);
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData("w", "x", "y")]
    [InlineData("General.Basics", "x", "y")]
    [InlineData("General.Basics", "General.Basics.Generators", "y")]
    [InlineData("w", "General.Basics.Generators", "y")]
    [InlineData("w", "General.Basics.Generators", "IdsGenerator")]
    [InlineData("w", "x", "IdsGenerator")]
    [InlineData("General.Basics", "x", "IdsGenerator")]
    public void GetGenericTypeFromNamesInfos_WhenTypeDoesNotExist_ShouldReturnNull(string maybeUnexistingAssemblyName, string maybeUnexistingNameSpace, string maybeUnexistingClassName)
    {
        //--- Arrange ---
        Type[] genericParamsType = new Type[] { typeof(int) };

        //--- Act ---
        Type? type = Type_.GetGenericTypeFromNamesInfos(maybeUnexistingAssemblyName, maybeUnexistingNameSpace, maybeUnexistingClassName, genericParamsType);

        //--- Assert ---
        Assert.Null(type);
    }

    [Fact]
    public void GetGenericTypeFromNamesInfos_WhenGenericParametersTypeAreMissing_ShouldThrowAMissingGenericParametersTypeException()
    {
        //--- Arrange ---
        string genericClassAssemblyName = currentAssemblyName;
        string genericClassNamespace = currentNamespace;
        string genericClassName = "MyGenericParentClass";
        Type[] genericParamsType = new Type[] { }; //<<<<

        var methodName = $"{nameof(Type_)}.{nameof(Type_.GetGenericTypeFromNamesInfos)}";

        //--- Act & Assert ---
        var ex = Assert.Throws<MissingGenericParametersTypeException>(() =>
            Type_.GetGenericTypeFromNamesInfos(genericClassAssemblyName, genericClassNamespace, genericClassName, genericParamsType));

        var expectedMessage = string.Format(MissingGenericParametersTypeException.MESSAGE_FORMAT, methodName, $"{genericClassNamespace}.{genericClassName}");
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion GetGenericTypeFromNamesInfos
}



#region Non generic definitions
interface IMyInterface1
{

}

class MyParentClass
{

}
class MyChildClass : MyParentClass
{

}

abstract class MyAbstractClass
{

}

record MyRecord
{

}

struct MyStruct
{

}
#endregion Non generic definitions



#region Generic definitions
class MyGenericClass<T, U>
{

}
interface IMyGenericInterface<T>
{

}

class MyGenericParentClass<T, U, V>
{

}
class MyGenericChildClass<X, Y, Z> : MyGenericParentClass<X, Y, Z>
{

}

abstract class MyGenericAbstractClass<U, V>
{

}

record MyGenericRecord<T, V>
{

}

struct MyGenericStruct<T, X>
{

}
#endregion Generic definitions