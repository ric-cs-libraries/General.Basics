using System.Reflection;

using Xunit;


using General.Basics.ReflectionExtended.Extensions;


namespace General.Basics.ReflectionExtended.Extensions.UnitTests;

public class MethodInfoExtensionTests
{
    readonly string currentNamespace;

    public MethodInfoExtensionTests()
    {
        Type currentClassType = typeof(TypeExtensionTests);
        currentNamespace = currentClassType.Namespace!;
    }

    #region GetParametersList_
    [Fact]
    public void GetParametersList_WhenWithoutFullInfos_ShouldReturnTheCorrectParametersListInfos()
    {
        //--- Arrange ---
        Type myClass = typeof(MyClass);
        MethodInfo method1 = myClass.GetMethod("Method1")!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool,string,DateTime>>);
        MethodInfo genericClassMethod = myGenericClass.GetMethod("Method")!;

        bool fullInfos = false;


        //--- Act ---
        string result1 = method1.GetParametersList_(fullInfos);
        string result2 = genericClassMethod.GetParametersList_(fullInfos);

        //--- Assert ---
        Assert.Equal($"System.String, System.Nullable<Boolean>, {currentNamespace}.MyClass, System.Int32", result1);
        Assert.Equal($"System.Int32, {currentNamespace}.IMyClass<Boolean,String,DateTime>, System.Boolean", result2);
    }

    [Fact]
    public void GetParametersList_WhenWithFullInfos_ShouldReturnTheCorrectParametersListInfos()
    {
        //--- Arrange ---
        Type myClass = typeof(MyClass);
        MethodInfo method1 = myClass.GetMethod("Method1")!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool, string, DateTime>>);
        MethodInfo genericClassMethod = myGenericClass.GetMethod("Method")!;

        bool fullInfos = true;


        //--- Act ---
        string result1 = method1.GetParametersList_(fullInfos);
        string result2 = genericClassMethod.GetParametersList_(fullInfos);

        //--- Assert ---
        Assert.Equal($"System.String p1, System.Nullable<Boolean> p2, {currentNamespace}.MyClass p3, Optional System.Int32 p4", result1);
        Assert.Equal($"System.Int32 p1, {currentNamespace}.IMyClass<Boolean,String,DateTime> p2, System.Boolean p3", result2);
    }
    #endregion GetParametersList_

    #region GetFullName_
    [Fact]
    public void GetFullName__WhenFullInfosParamIsPassedToFalse_ShouldReturnSignatureWithoutParametersListFullInfos()
    {
        //--- Arrange ---
        string myClassName = "MyClass";
        Type myClass = typeof(MyClass);

        string method1Name = "Method1";
        string method2Name = "Method2";
        MethodInfo method1 = myClass.GetMethod(method1Name)!;
        MethodInfo method2 = myClass.GetMethod(method2Name)!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool, string, DateTime>>);
        string myGenericClassName = "MyClass";
        string method3Name = "Method";
        MethodInfo genericClassMethod = myGenericClass.GetMethod(method3Name)!;

        bool fullInfos = false;


        //--- Act ---
        string result1 = method1.GetFullName_(fullInfos);
        string result2 = method2.GetFullName_(fullInfos);
        string result3 = genericClassMethod.GetFullName_(fullInfos);

        //--- Assert ---
        Assert.Equal($"System.Void {currentNamespace}.{myClassName}.{method1Name}(System.String, System.Nullable<Boolean>, {currentNamespace}.{myClassName}, System.Int32)", result1);
        Assert.Equal($"System.String {currentNamespace}.{myClassName}.{method2Name}(System.DateTime)", result2);
        Assert.Equal($"System.Void {currentNamespace}.{myGenericClassName}<Int32,IMyClass<Boolean,String,DateTime>>.{method3Name}(System.Int32, {currentNamespace}.IMyClass<Boolean,String,DateTime>, System.Boolean)", result3);
    }

    [Fact]
    public void GetFullName__WhenFullInfosParamIsntPassed_ShouldReturnSignatureWithoutParametersListFullInfos()
    {
        //--- Arrange ---
        string myClassName = "MyClass";
        Type myClass = typeof(MyClass);

        string method1Name = "Method1";
        string method2Name = "Method2";
        MethodInfo method1 = myClass.GetMethod(method1Name)!;
        MethodInfo method2 = myClass.GetMethod(method2Name)!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool, string, DateTime>>);
        string myGenericClassName = "MyClass";
        string method3Name = "Method";
        MethodInfo genericClassMethod = myGenericClass.GetMethod(method3Name)!;


        //--- Act ---
        string result1 = method1.GetFullName_();
        string result2 = method2.GetFullName_();
        string result3 = genericClassMethod.GetFullName_();

        //--- Assert ---
        Assert.Equal($"System.Void {currentNamespace}.{myClassName}.{method1Name}(System.String, System.Nullable<Boolean>, {currentNamespace}.{myClassName}, System.Int32)", result1);
        Assert.Equal($"System.String {currentNamespace}.{myClassName}.{method2Name}(System.DateTime)", result2);
        Assert.Equal($"System.Void {currentNamespace}.{myGenericClassName}<Int32,IMyClass<Boolean,String,DateTime>>.{method3Name}(System.Int32, {currentNamespace}.IMyClass<Boolean,String,DateTime>, System.Boolean)", result3);
    }

    [Fact]
    public void GetFullName__WhenFullInfosParamIsPassedToTrue_ShouldReturnSignatureWithParametersListFullInfos()
    {
        //--- Arrange ---
        string myClassName = "MyClass";
        Type myClass = typeof(MyClass);

        string method1Name = "Method1";
        string method2Name = "Method2";
        MethodInfo method1 = myClass.GetMethod(method1Name)!;
        MethodInfo method2 = myClass.GetMethod(method2Name)!;

        Type myGenericClass = typeof(MyClass<int, IMyClass<bool, string, DateTime>>);
        string myGenericClassName = "MyClass";
        string method3Name = "Method";
        MethodInfo genericClassMethod = myGenericClass.GetMethod(method3Name)!;

        bool fullInfos = true;


        //--- Act ---
        string result1 = method1.GetFullName_(fullInfos);
        string result2 = method2.GetFullName_(fullInfos);
        string result3 = genericClassMethod.GetFullName_(fullInfos);

        //--- Assert ---
        Assert.Equal($"System.Void {currentNamespace}.{myClassName}.{method1Name}(System.String p1, System.Nullable<Boolean> p2, {currentNamespace}.{myClassName} p3, Optional System.Int32 p4)", result1);
        Assert.Equal($"System.String {currentNamespace}.{myClassName}.{method2Name}(System.DateTime dt)", result2);
        Assert.Equal($"System.Void {currentNamespace}.{myGenericClassName}<Int32,IMyClass<Boolean,String,DateTime>>.{method3Name}(System.Int32 p1, {currentNamespace}.IMyClass<Boolean,String,DateTime> p2, System.Boolean p3)", result3);
    }

    #endregion GetFullName_

    #region IsReturnTypeATask_
    [Fact]
    public void IsReturnTypeATask_WhenReturnTypeIsATaskOrTaskOfT_ShouldReturnTrue()
    {
        //--- Arrange ---
        Type typeOfMyClassAsync = typeof(MyClassAsync);
        MethodInfo method1 = typeOfMyClassAsync.GetMethod("GetX")!;
        MethodInfo method2 = typeOfMyClassAsync.GetMethod("GetStr")!;

        //--- Act ---
        var result1 = method1.IsReturnTypeATask_();
        var result2 = method2.IsReturnTypeATask_();

        //--- Assert ---
        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void IsRetudrnTypeATask_WhenReturnTypeIsNotATaskOrTaskOfT_ShouldReturnFalse()
    {
        //--- Arrange ---
        Type typeOfMyClass = typeof(MyClass);
        MethodInfo method1 = typeOfMyClass.GetMethod("Method1")!;
        MethodInfo method2 = typeOfMyClass.GetMethod("Method2")!;

        //--- Act ---
        var result1 = method1.IsReturnTypeATask_();
        var result2 = method2.IsReturnTypeATask_();

        //--- Assert ---
        Assert.False(result1);
        Assert.False(result2);
    }
    #endregion IsReturnTypeATask_
}