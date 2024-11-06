using Shared.Core.Domain.Rules.BaseEntityRules;
using Shared.Core.Drivens.Validators.Base;

namespace Shared.Core.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? LastUpdatedAt { get; set; } = default;
}

public abstract class BaseEntityRule<TEntity, TEntityRule> : BaseNoParamValidator<TEntity, TEntityRule> 
    where TEntityRule : BaseEntityRule<TEntity, TEntityRule>, new()
    where TEntity : BaseEntity
{
    protected BaseEntityRule()
    {
        RuleFor(e => e.Id)
            .SetValidator(IdRule.Instance);
        
        RuleFor(e => e.CreatedAt)
            .SetValidator(CreatedAtRule.Instance);
    }
}