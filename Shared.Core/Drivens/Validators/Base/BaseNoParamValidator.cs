using FluentValidation;
using FluentValidation.Results;

namespace Shared.Core.Drivens.Validators.Base;

/// <typeparam name="TVal">The type of the object being validated</typeparam>
/// <typeparam name="TDerived">
/// The type must have a parameterless constructor, 
/// which is the reason it's used with <see cref="BaseNoParamValidator{TVal,TDerived}"/>.
/// </typeparam>
public abstract class BaseNoParamValidator<TVal, TDerived> : AbstractValidator<TVal>
    where TDerived : BaseNoParamValidator<TVal, TDerived>, new()
{
    protected static readonly TDerived Instance = new();

    public static ValidationResult ExecuteValidation(TVal value)
        => Instance.Validate(value);
    
    public static void ExecuteValidationAndThrow(TVal value)
        => Instance.ValidateAndThrow(value);
}