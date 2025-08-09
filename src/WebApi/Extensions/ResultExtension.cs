using Domain.Common;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult Match(this Result result, Func<IActionResult> onSuccess, Func<Result, IActionResult> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result);
        }

        public static IActionResult Match<T>(this Result<T> result, Func<T, IActionResult> onSuccess, Func<Result, IActionResult> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
        }

        public static IActionResult CustomResult(Result result)
        {
            if (result.IsFailure || result.Error == null)
                throw new InvalidOperationException();

            var error = result.Error;

            return error.Type switch
            {
                ErrorType.NotFound => new NotFoundObjectResult(new { error = error.Message }),
                ErrorType.Validation => new BadRequestObjectResult(new { error = error.Message }),
                ErrorType.Conflict => new ConflictObjectResult(new { error = error.Message }),
                ErrorType.Unauthorized => new UnauthorizedObjectResult(new { error = error.Message }),
                ErrorType.Forbidden => new ForbidResult(),
                _ => new ObjectResult(new { error = error.Message }) { StatusCode = 500 }
            };
        }
    }
}
