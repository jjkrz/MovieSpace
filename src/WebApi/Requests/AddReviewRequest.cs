using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebApi.Requests
{
    public sealed record AddReviewRequest(
        string Content)
    {
    }
}
