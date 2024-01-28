namespace UrlShortener.Services.Static
{
    public static class UlrValidationService
    {
        public static bool IsUrlValid(string url, UriKind urlKind)
        {
            return Uri.TryCreate(url, urlKind, out _);
        }
    }
}
