namespace General.Basics.Extensions;

public static class EnumExtension
{
    /// <returns>null if the Enum is empty !</returns>
    public static TEnum? GetMaxValue<TEnum>()
        where TEnum : struct, Enum
    {
        IEnumerable<TEnum> enumValues = Enum.GetValues<TEnum>();
        if (enumValues.Any())
        {
            IEnumerable<int> enumAllEnumValues = enumValues.Cast<int>().OrderByDescending(i => i);
            int maxValue = enumAllEnumValues.First();
            return (TEnum)ToValueOf<TEnum>(maxValue)!;
        }
        return null;
    }

    /// <returns>null if can not be converted to a value from the TEnum enum</returns>
    public static TEnum? ToValueOf<TEnum>(object valueToBeConverted)
        where TEnum : struct, Enum
    {
        string valueToBeConvertedAsString = $"{valueToBeConverted}";
        if (Enum.TryParse($"{valueToBeConverted}", out TEnum valueConvertedToAPotentialTEnumValue))
        {
            //But in fact, even if we pass here, valueConvertedToAPotentialTEnumValue may not exist in TEnum !! So we have to check that :
            if (Enum.IsDefined(typeof(TEnum), valueConvertedToAPotentialTEnumValue))
                return valueConvertedToAPotentialTEnumValue;
        }
        return null;
    }
}