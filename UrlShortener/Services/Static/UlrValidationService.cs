namespace UrlShortener.Services.Static
{
    public static class UlrValidationService
    {
        public static bool IsUrlValid(string url, UriKind urlKind)
        {
            if(Uri.TryCreate(url, urlKind, out var result))
            {
                return result.Scheme is "https" or "http";
            }

            return false;
        }
    }
}
