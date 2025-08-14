using Domain.Common;

namespace Domain.Movies.Errors
{
    public static class MovieErrors
    {
        public static Error RatingScoreOutOfRange(int Value) => new Error(ErrorType.Validation, $"Provided score value: {Value} is out of range (1-10)");

    }
}
