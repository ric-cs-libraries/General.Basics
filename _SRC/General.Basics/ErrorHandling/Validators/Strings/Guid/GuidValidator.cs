using General.Basics.Converters.Guids;


namespace General.Basics.ErrorHandling.Validators;

public record GuidValidator : MandatoryStringValidator
{
    public override bool IsValid(string? str)
    {
        Result result = Validate(str, string.Empty);
        return result.IsSuccess;
    }

    public override Result Validate(string? str, string subjectLabel)
    {
        Result result = base.Validate(str, subjectLabel);

        if (result.IsSuccess)
        {
            result = ToGuid.FromString(str!, subjectLabel);
        }

        return result;
    }
}
