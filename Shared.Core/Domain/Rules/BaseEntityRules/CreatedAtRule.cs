using FluentValidation;
using Shared.Core.Drivens.Validators.Base;

namespace Shared.Core.Domain.Rules.BaseEntityRules;

public class CreatedAtRule : BaseNoParamValidator<DateTime, CreatedAtRule>
{
    public CreatedAtRule()
    {
        RuleFor(createdAt => createdAt)
            .LessThanOrEqualTo(_ => DateTime.Now)
            .WithMessage("must be less than or equal to current time.");
    }
}