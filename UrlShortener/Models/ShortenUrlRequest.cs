using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Models
{
    public class ShortenUrlRequest
    {
        [FromBody]
        public string Url { get; set; } = string.Empty;
    }
}
