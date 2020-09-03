namespace Synergy.Sample.Web.API.Tests.Infrastructure
{
    public static class FormattingExtensions
    {
        public static string QuoteOrNull(this string? value)
        {
            if (value == null)
            {
                return "null";
            }

            return $"\"{value}\"";
        }
    }
}