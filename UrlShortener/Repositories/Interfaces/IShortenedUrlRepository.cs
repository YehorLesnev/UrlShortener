using UrlShortener.Entities;
using UrlShortener.Repositories.Implementations;

namespace UrlShortener.Repositories.Interfaces
{
    public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
    {
        Task<ShortenedUrl?> GetByCodeAsync(string code,  CancellationToken cancellationToken = default);
        Task<ShortenedUrl?> GetByLongUrlAsync(string longUrl,  CancellationToken cancellationToken = default);
    }
}
