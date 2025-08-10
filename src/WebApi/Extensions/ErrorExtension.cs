using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class ErrorExtension
    {
        public static ProblemDetails ToProblemDetails(this Error error)
        {
            return new ProblemDetails
            {
                Title = error.Type.ToString(),
                Status = GetStatusCode(error.Type),
                Detail = error.Message
            };
        }

        public static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.None => StatusCodes.Status200OK,
                ErrorType.NullValue => StatusCodes.Status400BadRequest,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.BadRequest => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Dependency => StatusCodes.Status424FailedDependency,
                ErrorType.Timeout => StatusCodes.Status408RequestTimeout,
                ErrorType.Unavailable => StatusCodes.Status503ServiceUnavailable,
                ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }

    }
}
