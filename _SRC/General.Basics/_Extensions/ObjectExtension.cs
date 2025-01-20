using System.Reflection;

namespace General.Basics.Extensions;

public static class ObjectExtension
{
    public static Dictionary<string, string> GetSomePropertiesNameAndValue_(this object obj, HashSet<string> propertiesOnlyOfName, HashSet<Type> propertiesOnlyOfType)
    {
        Dictionary<string, string> result = new();

        Type typeOfObj = obj.GetType();
        foreach (string propertyName in propertiesOnlyOfName)
        {
            PropertyInfo? propertyInfo = typeOfObj.GetProperty(propertyName); //La property visée doit être public.
            if (propertyInfo != null && propertiesOnlyOfType.Contains(propertyInfo.PropertyType))
            {
                var propertyValue = propertyInfo.GetValue(obj);
                //Si la ligne ci-dessus échoue, essayer avec les 2 lignes suivantes :
                //var objWithRealType = Convert.ChangeType(obj, typeOfObj); //Cast dynamique plus souple
                //var propertyValue = propertyInfo.GetValue(objWithRealType);

                result.Add(propertyName, propertyValue!.ToString()!);
            }
        }

        return result;
    }
}