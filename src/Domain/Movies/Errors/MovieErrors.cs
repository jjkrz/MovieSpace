using System.Data;
using System;
using Domain.Common;

namespace Domain.Movies.Errors
{
    public static class MovieErrors
    {
        public static Error RatingScoreOutOfRange(int Value) => new Error(ErrorType.Validation, $"Provided score value: {Value} is out of range (1-10)");
        public static Error DuplicateGenre(string genre) => new Error(ErrorType.Conflict, $"Genre '{genre}' already exists in the movie's genre list.");
        public static Error NullCharacterName => new Error(ErrorType.Validation, "Character name cannot be null for roles that require it (e.g., Actor).");
        public static Error DuplicateCastMember => new Error(ErrorType.Conflict, "Cast member already exists in this movie");
    }
}
