using System.Linq;
using System.Reflection;

using General.Basics.Reflection.POO;

namespace General.Basics.Reflection.Extensions;


public static class AssemblyExtension
{
    #region Reflection
    public static List<Class> GetClassesImplementingTheInterface_(this Assembly assembly, Interface @interface)
    {
        List<Class> classes = GetClassesImplementingTheInterface(assembly, @interface);
        return classes;
    }
    public static List<Class> GetConcreteClassesImplementingTheInterface_(this Assembly assembly, Interface @interface)
    {
        List<Class> classes = GetClassesImplementingTheInterface(assembly, @interface, type => !type.IsAbstract);
        return classes;
    }
    private static List<Class> GetClassesImplementingTheInterface(Assembly assembly, Interface @interface, Func<Type, bool>? additionalFilter = null)
    {
        IEnumerable<Type> types = assembly.GetTypes()
                                  .Where(type => type.Implements_(@interface));

        if (additionalFilter is not null)
        {
            types = types.Where(additionalFilter);
        }

        IEnumerable<Class> classes = types.Select(type => (type.IsAbstract) ? new AbstractClass(type) : new Class(type));

        return classes.ToList();
    }
    #endregion Reflection
}
