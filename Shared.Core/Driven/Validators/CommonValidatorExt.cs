using FluentValidation;

namespace Shared.Core.Driven.Validators;

public static class CommonValidatorExt
{
    public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty();
    }
}