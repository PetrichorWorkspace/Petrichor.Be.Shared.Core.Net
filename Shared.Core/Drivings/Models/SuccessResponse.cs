namespace Shared.Core.Drivings.Models;

public class SuccessResponse<TResponse> : BaseResponse
{
    public TResponse? Response { get; init; } = default;
}