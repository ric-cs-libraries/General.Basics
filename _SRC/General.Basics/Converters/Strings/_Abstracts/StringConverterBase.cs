using General.Basics.Converters.Strings.Interfaces;

namespace General.Basics.Converters.StringConverters.Abstracts;

public abstract class StringConverterBase : IStringConverter
{
    private readonly IStringConverter? previousStringConverter;
    protected abstract string SpecificConvert(string str);

    protected StringConverterBase(IStringConverter? previousStringConverter = null)
    {
        this.previousStringConverter = previousStringConverter;
    }

    public string Convert(string str)
    {
        if (previousStringConverter is not null)
            str = previousStringConverter.Convert(str);

        str = SpecificConvert(str);

        return str;
    }
}
