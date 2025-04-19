using General.Basics.Extensions;
using General.Basics.Reflection.Extensions;


namespace General.Basics.ErrorHandling;

public record Error //With Mandatory Code
{
    protected virtual bool IsCodeMandatory => true;
    public string Code { get; }

    public string DebugMessageTemplate { get; }
    public IEnumerable<string?>? PlaceholderValues { get; }
    public string DebugMessage { get; private set; } = null!;

    public virtual string Type => GetType().GetName_();
    public string Kind => Type + ((Code.IsEmpty_()) ? string.Empty : $" '{Code}'");


    /// <exception cref="ErrorCodeIsRequiredException"></exception>
    public Error(string code, string debugMessageTemplate, IEnumerable<string?>? placeholderValues)
    {
        Code = code.Trim();
        if (IsCodeMandatory && Code == string.Empty)
        {
            throw new ErrorCodeIsRequiredException();
        }

        DebugMessageTemplate = debugMessageTemplate;
        PlaceholderValues = placeholderValues ?? Enumerable.Empty<string>();
        SetDebugMessage();
    }

    public Error(string code, string debugMessageTemplate = "") : this(code, debugMessageTemplate, null)
    {
    }

    public Error(string code, Error error) : this(code, $"{error.DebugMessage} (from {error.Type})")
    {
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