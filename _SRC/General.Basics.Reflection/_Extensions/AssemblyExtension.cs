using System.Reflection;

using General.Basics.Reflection.POO;

namespace General.Basics.Reflection.Extensions;


public static class AssemblyExtension
{
    #region Reflection
    public static List<Class> GetClassesImplementingTheInterface(this Assembly instance, Interface @interface)
    {
        List<Class> classes = new();
        Class classToAdd = null!;

        Type[] assemblyTypes = instance.GetTypes();
        foreach(Type type in assemblyTypes)
        {
            if (type.IsClass && @interface.Type.IsAssignableFrom(type))
            {
                classToAdd = (type.IsAbstract) ? new AbstractClass(type) : new Class(type);
                classes.Add(classToAdd);
            }
        }

        return classes;
    }
    #endregion Reflection
}
