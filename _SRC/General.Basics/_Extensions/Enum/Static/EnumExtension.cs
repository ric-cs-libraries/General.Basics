namespace General.Basics.Extensions;

public static class EnumExtension
{
    public static TEnum GetMaxValue<TEnum>()
        where TEnum : struct, Enum
    {
        TEnum[] enumAllEnumValues = Enum.GetValues<TEnum>();
        TEnum lastEnumValue = enumAllEnumValues[^1];
        return lastEnumValue;
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