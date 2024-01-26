using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Repositories.Implementations
{
    public class ShortenedUrlRepository(ApplicationDbContext dbContext)
        : Repository<ShortenedUrl>(dbContext), IShortenedUrlRepository
    {
        public async Task<ShortenedUrl?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAll(u => u.Code == code).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ShortenedUrl?> GetByLongUrlAsync(string longUrl, CancellationToken cancellationToken = default)
        {
            return await GetAll(u => u.Equals(longUrl)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
