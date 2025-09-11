using Domain.Common;

namespace Infrastructure.Http_Clients
{
    public static class HttpClientErrors
    {
        public static Error ImdbRatingFetchingFailed => new Error(ErrorType.Unavailable, "Imdb rating fetching failed");
    }
}
