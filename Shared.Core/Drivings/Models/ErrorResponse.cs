using FluentValidation.Results;
using Shared.Core.Domain.Exceptions;

namespace Shared.Core.Drivings.Models;

public class ErrorResponse : BaseResponse
{
    public const string FluentValidationMessagePartToRemove = nameof(FluentValidationMessagePartToRemove);
    
    public required List<Error> Errors { get; init; }

    public static ErrorResponse ToErrorResponse(ValidationResult validationResult)
    {
        return new ErrorResponse
        {
            Message = "Errors have occurred.", // TODO find a way to store the response message
            Errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .Select(group =>
                {
                    #region Validation

                    if (string.IsNullOrWhiteSpace(group.Key))
                        // TODO log here
                        throw new Exception($"{nameof(group.Key)}/{nameof(ValidationFailure.PropertyName)} is required.");

                    #endregion
                    
                    return new Error
                    {
                        Field = group.Key,
                        Errors = group.Select(error =>
                        {
                            #region Validation

                            if (string.IsNullOrWhiteSpace(error.ErrorCode))
                                // TODO log here
                                throw new Exception($"{nameof(error.ErrorCode)} is required.");

                            if (string.IsNullOrWhiteSpace(error.ErrorMessage))
                                // TODO log here
                                throw new Exception($"{nameof(error.ErrorMessage)} is required.");

                            if (error.ErrorMessage.Contains(error.PropertyName))
                                // TODO log here
                                throw new Exception($"{nameof(error.ErrorMessage)} should not contain {error.PropertyName}.");

                            #endregion

                            #region Custom FluentValidation Error Message

                            error.ErrorMessage = error.ErrorMessage.Replace($"'{FluentValidationMessagePartToRemove}' ", string.Empty);

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
    
    public static ErrorResponse ToErrorResponse(string fieldName, IExcHasErrorCode exc)
    {
        #region Validation

        if (string.IsNullOrWhiteSpace(fieldName))
            // TODO log here
            throw new Exception($"{nameof(fieldName)} is required.");
        
        if (string.IsNullOrWhiteSpace(exc.Code))
            // TODO log here
            throw new Exception($"{nameof(exc.Code)} is required.");
        
        if (string.IsNullOrWhiteSpace(exc.Message))
            // TODO log here
            throw new Exception($"{nameof(exc.Message)} is required.");
        
        if (exc.Message.Contains(fieldName))
            // TODO log here
            throw new Exception($"{nameof(exc.Message)} should not contain {fieldName}.");
        
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