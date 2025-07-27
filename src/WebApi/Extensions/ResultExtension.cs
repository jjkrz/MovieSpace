using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static TOut Match<TOut>(
            this Result result,
            Func<TOut> onSuccess,
            Func<Result, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result);
        }

        public static TOut Match<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> onSuccess,
            Func<Result<TIn>, TOut> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
        }

        public static IActionResult ToProblem<T>(this Result<T> result, ControllerBase controller)
        {
            var error = result.Error!;
            var problem = new ProblemDetails
            {
                Detail = error.Message,
                Status = GetStatusCode(error.Type),
                Title = error.Type.ToString()
            };

            return controller.StatusCode(problem.Status.Value, problem);
        }

        public static IActionResult ToProblem(this Result result, ControllerBase controller)
        {
            var error = result.Error!;
            var problem = new ProblemDetails
            {
                Detail = error.Message,
                Status = GetStatusCode(error.Type),
                Title = error.Type.ToString()
            };

            return controller.StatusCode(problem.Status.Value, problem);
        }

        private static int GetStatusCode(ErrorType type)
        {
            return type switch
            {
                ErrorType.Validation => 400,
                ErrorType.BadRequest => 400,
                ErrorType.NotFound => 404,
                ErrorType.Conflict => 409,
                ErrorType.Unauthorized => 401,
                ErrorType.Forbidden => 403,
                ErrorType.Dependency => 424,       
                ErrorType.Timeout => 408,          
                ErrorType.Unavailable => 503,      
                ErrorType.Unexpected => 500,       
                _ => 500
            };
        }
    }
}
