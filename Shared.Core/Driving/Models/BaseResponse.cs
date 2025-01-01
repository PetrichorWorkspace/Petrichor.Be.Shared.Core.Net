using System.Text.Json.Serialization;

namespace Shared.Core.Driving.Models;

[Serializable]
public abstract class BaseResponse
{
    [JsonPropertyName("_message")]
    [JsonPropertyOrder(-10)]
    public required string Message { get; init; }
}