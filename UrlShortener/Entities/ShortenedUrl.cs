using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Entities
{
    public sealed class ShortenedUrl(Guid id) : Entity(id)
    {
        public string LongUrl { get; set; } = string.Empty;

        [MaxLength(Constants.Constants.MaxLengthOfShortenedUrl)]
        public string ShortUrl { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    
        public DateTime CreatedOnUtc { get; set; }

        public ShortenedUrl(Guid id, string longUrl,  string shortUrl, string code,  DateTime createdOnUtc)
            : this(id)
        {
            this.LongUrl = longUrl;
            this.ShortUrl = shortUrl;
            this.CreatedOnUtc = createdOnUtc;
            this.Code = code;
        }
    }
}
