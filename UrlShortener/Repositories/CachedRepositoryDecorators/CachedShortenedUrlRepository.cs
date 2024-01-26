using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using UrlShortener.Entities;
using UrlShortener.Repositories.Implementations;
using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Repositories.CachedRepositoryDecorators
{
    public class CachedShortenedUrlRepository(ShortenedUrlRepository decorated, IMemoryCache memoryCache) : IShortenedUrlRepository
    {
        private readonly ShortenedUrlRepository _decorated = decorated;
        private readonly IMemoryCache _memoryCache = memoryCache;

        public IQueryable<ShortenedUrl> GetAll()
        {
            return _decorated.GetAll();
        }

        public IQueryable<ShortenedUrl> GetAll(Expression<Func<ShortenedUrl, bool>> conditions)
        {
            return _decorated.GetAll(conditions);
        }

        public IQueryable<ShortenedUrl> GetAllWith(Expression<Func<ShortenedUrl, bool>> conditions, params Expression<Func<ShortenedUrl, object>>[] includeExpressions)
        {
            return _decorated.GetAllWith(conditions, includeExpressions);
        }

        public Task<bool> AnyAsync(Expression<Func<ShortenedUrl, bool>> conditions)
        {
            return _decorated.AnyAsync(conditions);
        }

        public bool Any(Expression<Func<ShortenedUrl, bool>> conditions)
        {
            return _decorated.Any(conditions);
        }

        public ShortenedUrl? GetById(Guid id)
        {
            return _decorated.GetById(id);
        }

        public Task<ShortenedUrl?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string key = $"url-{id}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                    return _decorated.GetByIdAsync(id, cancellationToken);
                });
        }

        public void Insert(ShortenedUrl record)
        {
            _decorated.Insert(record);
        }

        public void InsertRange(IEnumerable<ShortenedUrl> records)
        {
            _decorated.InsertRange(records);
        }

        public void Update(ShortenedUrl record)
        {
            _decorated.Update(record);
        }

        public void UpdateRange(IEnumerable<ShortenedUrl> records)
        {
            _decorated.UpdateRange(records);
        }

        public void Delete(ShortenedUrl record)
        {
            _decorated.Delete(record);
        }

        public void DeleteIfNotNull(ShortenedUrl? record)
        {
            _decorated?.DeleteIfNotNull(record);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _decorated.DeleteAsync(id, cancellationToken);
        }

        public void DeleteRange(IEnumerable<ShortenedUrl> entities)
        {
            _decorated.DeleteRange(entities);
        }

        public Task CommitAsync()
        {
            return _decorated.CommitAsync();
        }

        public void Commit()
        {
            _decorated.Commit();
        }

        public Task<ShortenedUrl?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            string key = $"url-{code}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                    return _decorated.GetByCodeAsync(code, cancellationToken);
                });
        }

        public Task<ShortenedUrl?> GetByLongUrlAsync(string longUrl, CancellationToken cancellationToken = default)
        {
            string key = $"url-{longUrl}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                    return _decorated.GetByCodeAsync(longUrl, cancellationToken);
                });
        }
    }
}
