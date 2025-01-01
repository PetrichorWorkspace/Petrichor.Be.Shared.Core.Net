namespace Shared.Core.Domain.Exceptions;

public interface IExcHasErrorCode
{
    public string? TargetPropertyName { get; }

    public string Code { get; }
    public string Message { get; }
}