using Xunit;


using General.Basics.Extensions;
using General.Basics.Reflection.Extensions;

using General.Basics.Reflection.DynamicCalls.Abstracts;

using General.Basics.Reflection.DynamicCalls;


namespace General.Basics.Reflection.DynamicCalls.UnitTests;

public class UnexistingMethodExceptionTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Some details")]
    public void Instanciation__TheExceptionShouldContainTheCorrectMessage(string errorDetails)
    {
        Type type = typeof(MyClass);
        var methodName = "SomeUnexistingMethod";
        int? i = null;
        Type?[] parametersType = new Type?[] { i?.GetType(), typeof(int?), typeof(bool), typeof(MyClass) };

        var ex = new UnexistingMethodException(type, methodName, parametersType, errorDetails);

        IEnumerable<string> parametersTypeList = parametersType.Select(type_ => type_?.GetFullName_() ?? UnexistingMethodException.TYPE_FOR_NULL_PARAMS);
        string parametersTypeList_ = string.Join(", ", parametersTypeList);

        string methodFullName = string.Join(".", new string[] { type.GetFullName_(), methodName });

        errorDetails = errorDetails.Trim();
        if (!errorDetails.IsEmpty_())
        {
            errorDetails = $" Detail : {errorDetails}.";
        }

        var expectedMessage = string.Format(UnexistingMethodException.MESSAGE_FORMAT, type.GetAssemblyName_(), methodFullName, parametersTypeList_, errorDetails);
        Assert.Equal(expectedMessage, ex.Message);
    }

    class MyClass
    {

    }
}

