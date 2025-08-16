namespace Domain.Common
{
    public record Error(ErrorType Type, string Message)
    {
        public static Error None = new(ErrorType.None, string.Empty);
        public static Error NullValue = new(ErrorType.NullValue, "The value cannot be null.");
        public static Error NotFound = new(ErrorType.NotFound, "The requested resource was not found.");
    }
}
