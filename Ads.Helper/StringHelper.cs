namespace Ads.Helper
{
    public static class StringHelper
    {
        public static string With(this string format, params object[] args) {
            return string.Format(format, args);
        }
    }
}