namespace Shared.Core.Driving.Models;

public class SuccessResponse<TData> : BaseResponse
{
    public TData? Data { get; init; } = default;
}