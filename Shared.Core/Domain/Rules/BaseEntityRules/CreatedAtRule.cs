using FluentValidation;

namespace Shared.Core.Domain.Rules.BaseEntityRules;

public static class CreatedAtRule
{
    public static IRuleBuilderOptions<T, DateTime> CreatedAtRuleValidator<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .LessThanOrEqualTo(_ => DateTime.Now)
            .WithMessage("must be less than or equal to current time.");
    }
}