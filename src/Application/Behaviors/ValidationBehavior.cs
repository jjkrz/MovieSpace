using System.Reflection;
using Domain.Common;
using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> _validators)
        {
            this._validators = _validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var messages = failures
                    .Select(f => $"{f.PropertyName}: {f.ErrorMessage}")
                    .ToList();

                var error = new Error(ErrorType.Validation, string.Join("; ", messages));

                if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var innerType = typeof(TResponse).GetGenericArguments()[0];

                    var method = typeof(Result)
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(m =>
                            m.Name == "Failure" &&
                            m.IsGenericMethod &&
                            m.GetParameters().Length == 1 &&
                            m.GetParameters()[0].ParameterType == typeof(Error));

                    var genericFailure = method!.MakeGenericMethod(innerType);

                    var result = genericFailure.Invoke(null, new object[] { error });

                    return (TResponse)result!;

                }

                if (typeof(TResponse) == typeof(Result))
                {
                    var failure = Result.Failure(error);
                    return (TResponse)(object)failure;
                }
            }

            return await next();
        }
    }
}
