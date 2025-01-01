using FluentValidation.Results;
using Shared.Core.Domain.Exceptions;

namespace Shared.Core.Driving.Models;

public record Error
{
    public required string Field { get; init; }
    public required List<ErrorContent> Errors { get; init; }
}

public record ErrorContent
{
    public required string Code { get; init; }
    public required string Message { get; init; }
}

public class ErrorResponse : BaseResponse
{
    public required List<Error> Errors { get; init; }

    public static ErrorResponse ToErrorResponse(ValidationResult validationResult)
    {
        return new ErrorResponse
        {
            Message = "Errors have occurred.", // TODO find a way to store the response message
            Errors = validationResult.Errors
                .GroupBy(failure => failure.PropertyName)
                .Select(group =>
                {
                    #region Validation

                    if (string.IsNullOrWhiteSpace(group.Key))
                        // TODO log here
                        throw new Exception($"{nameof(group.Key)}/{nameof(ValidationFailure.PropertyName)} is required.");

                    #endregion
                    
                    return new Error
                    {
                        // TODO field name is uppercase fist char
                        Field = group.Key,
                        Errors = group.Select(error =>
                        {
                            #region Validation

                            // if (string.IsNullOrWhiteSpace(error.ErrorCode))
                            //     // TODO log here
                            //     throw new Exception($"{nameof(error.ErrorCode)} is required.");
                            //
                            // if (string.IsNullOrWhiteSpace(error.ErrorMessage))
                            //     // TODO log here
                            //     throw new Exception($"{nameof(error.ErrorMessage)} is required.");
                            //
                            // if (error.ErrorMessage.Contains($"{error.PropertyName}")) // TODO this won't work: identity Id, not IdentityId
                            //     // TODO log here
                            //     throw new Exception($"{nameof(error.ErrorMessage)} must not contain the property name: {error.PropertyName}.");

                            #endregion

                            #region Custom FluentValidation Error Message

                            // error.ErrorMessage = error.ErrorMessage.Replace($"''", group.Key);

                            #endregion
                            
                            return new ErrorContent
                            {
                                Code = error.ErrorCode,
                                Message = error.ErrorMessage,
                            };
                        }).ToList()
                    };
                }).ToList()
        };
    }
    
    // TODO field name is uppercase fist char
    public static ErrorResponse ToErrorResponse<TExc>(string fieldName, TExc exc) where TExc : IExcHasErrorCode
    {
        #region Validation

        // if (string.IsNullOrWhiteSpace(fieldName))
        //     // TODO log here
        //     throw new Exception($"{nameof(fieldName)} is required.");
        //
        // if (string.IsNullOrWhiteSpace(exc.Code))
        //     // TODO log here
        //     throw new Exception($"{nameof(exc.Code)} is required.");
        //
        // if (string.IsNullOrWhiteSpace(exc.Message))
        //     // TODO log here
        //     throw new Exception($"{nameof(exc.Message)} is required.");
        //
        // if (!exc.Message.Contains($"'{fieldName}'"))
        //     // TODO log here
        //     throw new Exception($"{nameof(exc.Message)} must contain '{fieldName}'.");

        #endregion
        
        return new ErrorResponse
        {
            Message = "Errors have occurred.", // TODO find a way to store the response message
            Errors =
            [
                new Error
                {
                    Field = fieldName,
                    Errors = 
                    [
                        new ErrorContent
                        {
                            Code = exc.Code,
                            Message = exc.Message,
                        }
                    ]
                }
            ]
        };
    }
}