using General.Basics.Extensions;


namespace General.Basics.ErrorHandling;

public record Error
{
    public string Code { get; }

    public string DebugMessageTemplate { get; }
    public IEnumerable<string?>? PlaceholderValues { get; }
    public string DebugMessage { get; private set; } = null!;

    public virtual string Type => GetType().Name;
    protected string Kind => $"{Type} '{Code}'";

    public Error(string code, string debugMessageTemplate = "", IEnumerable<string?>? placeholderValues = null)
    {
        Code = code.Trim();
        if (Code == string.Empty)
        {
            throw new ErrorCodeIsRequiredException();
        }

        DebugMessageTemplate = debugMessageTemplate;
        PlaceholderValues = placeholderValues ?? Enumerable.Empty<string>();
        SetDebugMessage();
    }

    private void SetDebugMessage()
    {
        DebugMessage = string.Format(DebugMessageTemplate, PlaceholderValues!.ToArray());
    }

    public virtual string ToString_()
    {
        List<string> infos = new()
        {
            Kind
        };

        if (!DebugMessage.IsEmptyOrOnlySpaces_())
        {
            infos.Add($" : {DebugMessage}");
        }

        var result = string.Join("", infos);
        return result;
    }

    public sealed override string ToString() => ToString_(); //"sealed" pour permettre aux records enfants de bénéficier de cette redéf. de ToString().

    //public static implicit operator string(Error error) => error.ToString_();

}
