using FluentValidation;

namespace Shared.Core.Domain.Rules.BaseEntityRules;

public static class IdRule
{
    public const int MinLength = 6;
    public const int MaxLength = 255;

    public static IRuleBuilderOptions<T, string> IdRuleValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .MinimumLength(MinLength)
            .MaximumLength(MaxLength);
    }
}