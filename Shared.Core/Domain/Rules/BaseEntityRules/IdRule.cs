using FluentValidation;
using Shared.Core.Drivens.Validators.Base;

namespace Shared.Core.Domain.Rules.BaseEntityRules;

public class IdRule : BaseNoParamValidator<string, IdRule>
{
    public const int MinLength = 6;
    public const int MaxLength = 255;
    
    public IdRule()
    {
        RuleFor(id => id)
            .MinimumLength(MinLength)
            .MaximumLength(MaxLength);
    }
}