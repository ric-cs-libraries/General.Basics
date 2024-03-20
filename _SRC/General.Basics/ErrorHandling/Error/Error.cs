using General.Basics.Extensions;



namespace General.Basics.ErrorHandling;

public record Error
{
    public string Code { get; }
    public string DebugMessage { get; }
    public string RelatedFieldName { get; }

    public Error(string code, string debugMessage = "", string relatedFieldName = "")
    {
        Code = code.Trim();
        if (Code == string.Empty)
        {
            throw new ErrorCodeIsRequiredException();
        }

        DebugMessage = debugMessage;
        RelatedFieldName = relatedFieldName;
    }

    public override string ToString()
    {
        List<string> infos = new()
        {
            $"Error '{Code}'"
        };

        if (!DebugMessage.IsEmptyOrOnlySpaces_())
        {
            infos.Add($" : {DebugMessage}");
        }

        if (!RelatedFieldName.IsEmptyOrOnlySpaces_())
        {
            infos.Add($" (relatedFieldName : '{RelatedFieldName}')");
        }

        var result = string.Join("", infos);
        return result;
    }

    public static implicit operator string(Error error) => error.ToString();
}
