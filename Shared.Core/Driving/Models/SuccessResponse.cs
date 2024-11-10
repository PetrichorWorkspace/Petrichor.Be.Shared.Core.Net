namespace Shared.Core.Driving.Models;

public class SuccessResponse<TResponse> : BaseResponse
{
    public TResponse? Response { get; init; } = default;
}