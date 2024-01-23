using Azure.Core;

namespace UrlShortener.Services
{
    public class UlrValidationService
    {
        public static bool isUrlValid(string url, UriKind urlKind)
        {
            return Uri.TryCreate(url, urlKind, out _);
        }
    }
}
