using FluentValidation;
using Shared.Core.Domain.Rules.BaseEntityRules;

namespace Shared.Core.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? LastModifiedAt { get; set; } = default;
}

public abstract class BaseEntityRule<TEntity> : AbstractValidator<TEntity>
    where TEntity : BaseEntity
{
    protected BaseEntityRule()
    {
        RuleFor(e => e.Id)
            .IdRuleValidator();
        
        RuleFor(e => e.CreatedAt)
            .CreatedAtRuleValidator();
    }
}