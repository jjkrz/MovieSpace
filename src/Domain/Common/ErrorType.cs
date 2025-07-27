namespace Domain.Common
{
    public enum ErrorType
    {
        None,
        NullValue,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        BadRequest,
        Unexpected,
        Dependency,
        Timeout,
        Unavailable
    }
}
