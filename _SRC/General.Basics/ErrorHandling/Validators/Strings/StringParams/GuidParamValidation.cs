using System.Text.RegularExpressions;

using General.Basics.Extensions;


namespace General.Basics.ErrorHandling.Validators;

public record GuidParamValidation(string paramNameLike)
    : StringParamValidation(
        paramNameRegExp: CreateRegExp(paramNameLike),
        stringParamValueValidator : new GuidValidator()
    )
{
    private static Regex CreateRegExp(string paramNameLike)
    {
        const string regExpFormat = "\\b(\\w*{0}|{1}|{2})\\b";

        var upperFirstCharOnly = paramNameLike.CapitalizeFirst_();
        var fullUppercase = paramNameLike.ToUpper();
        var fullLowercase = paramNameLike.ToLower();

        var result = string.Format(regExpFormat, upperFirstCharOnly, fullUppercase, fullLowercase); //Ex. : "\\b(\\w*Id|ID|id)\\b"   (when paramNameLike=="id")
        return new Regex(result);
    }
}
