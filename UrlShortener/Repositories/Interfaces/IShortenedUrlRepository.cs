using UrlShortener.Entities;
using UrlShortener.Repositories.Implementations;

namespace UrlShortener.Repositories.Interfaces
{
    public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
    {
        Task<ShortenedUrl?> GetById(Guid id,  CancellationToken cancellationToken = default);
        Task<ShortenedUrl?> GetByCode(string code,  CancellationToken cancellationToken = default);
        Task<ShortenedUrl?> GetByLongUrl(string longUrl,  CancellationToken cancellationToken = default);
    }
}
