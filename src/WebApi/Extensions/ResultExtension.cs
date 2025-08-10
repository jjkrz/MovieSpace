using Domain.Common;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult Match(this Result result, Func<IActionResult> onSuccess)
        {
            if (result.IsSuccess) return onSuccess();

            var problem = result.Error.ToProblemDetails();
            return new ObjectResult(problem) { StatusCode = problem.Status };
        }

        public static IActionResult Match<T>(this Result<T> result, Func<T, IActionResult> onSuccess)
        {
            if (result.IsSuccess) return onSuccess(result.Value);

            var problem = result.Error.ToProblemDetails();
            return new ObjectResult(problem) { StatusCode = problem.Status };
        }
    }
}
