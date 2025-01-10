
namespace General.Basics.Extensions;

public static partial class IEnumerableExtension
{
    public static int? GetNearestInfValue_(this IEnumerable<int> enumerable, int value)
    {
        IEnumerable<int> eligibles = enumerable.Where(elementValue => elementValue <= value).OrderBy(elementValue => elementValue);
        int? nearestInfValue = (eligibles.Any()) ? eligibles.Last() : null;
        return nearestInfValue;
    }
}
