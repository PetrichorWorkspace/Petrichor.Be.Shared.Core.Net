using FluentValidation;
using Shared.Core.Domain.Rules.BaseEntityRules;

namespace Shared.Core.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? LastModifiedAt { get; set; } = null;

    public bool IsModified => LastModifiedAt is not null;
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