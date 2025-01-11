using General.Basics.Extensions;
using General.Basics.Reflection.Extensions;

namespace General.Basics.ReflectionExtended.POO.ErrorHandling;

public class ConcreteClassMustRedefineAtLeastOneSomeMethodsException : Exception
{
    public const string MESSAGE_FORMAT = "Concrete class '{0}', Must Redefine At Least one of these methods : {1} (predefined in class '{2}').";

    public override string Message { get; }

    public ConcreteClassMustRedefineAtLeastOneSomeMethodsException(Type concreteClassType, IEnumerable<string> methodsName, Type parentTypeThatDefinesThemAll)
    {
        string methodsList = methodsName.ToStringAsArray_();
        Message = string.Format(MESSAGE_FORMAT, concreteClassType.GetName_(), methodsList, parentTypeThatDefinesThemAll.GetName_());
    }
}
