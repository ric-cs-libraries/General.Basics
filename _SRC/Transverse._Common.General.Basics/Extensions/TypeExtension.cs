namespace Transverse._Common.General.Basics.Extensions;

public static class TypeExtension
{
    public static string GetSimpleName(this Type type)
    {
        var excludeFromThisChar = "`"; //Caractère "gênant" ajouté dans la GetType().Name, lorsque la classe est générique.
        var result = $"{type.Name.Split(excludeFromThisChar)[0]}";
        return result;
    }

}
