namespace Application.Services
{
    public static class AccessCodeGenerator
    {
        private static readonly Random _random = new();

        public static string GenerateUniqueAccessCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(
                Enumerable.Range(0, 6)
                    .Select(_ => chars[_random.Next(chars.Length)])
                    .ToArray());
        }
    }
}
