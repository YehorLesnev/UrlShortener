using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using UrlShortener.Entities;
using UrlShortener.Repositories.Implementations;
using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Repositories.CachedRepositoryDecorators
{
    public class CachedShortenedUrlRepository(
        ShortenedUrlRepository decorated, 
        IDistributedCache distributedCache,
        ApplicationDbContext dbContext
        ) : IShortenedUrlRepository
    {
        private readonly ShortenedUrlRepository _decorated = decorated;
        private readonly IDistributedCache _distributedCache = distributedCache;
        private readonly ApplicationDbContext _dbContext = dbContext;

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

        public async Task<ShortenedUrl?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string key = $"url-{id}";

            // get value from cache
            string? cachedUrl = await _distributedCache.GetStringAsync(
                key,
                cancellationToken);

            // if value is not present in cache
            if (string.IsNullOrEmpty(cachedUrl))
            {
                ShortenedUrl? url = await _decorated.GetByIdAsync(id, cancellationToken);

                if (url is null)
                {
                    return url;
                }

                // cache url in Redis
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(url),
                    cancellationToken);

                return url;
            }

            var urlFromCache = JsonConvert.DeserializeObject<ShortenedUrl>(
                cachedUrl,
                new JsonSerializerSettings()
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                });

            if (urlFromCache is not null)
            {
                _dbContext.Set<ShortenedUrl>().Attach(urlFromCache);
            }

            return urlFromCache;
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

        public async Task<ShortenedUrl?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            string key = $"url-{code}";

            // get value from cache
            string? cachedUrl = await _distributedCache.GetStringAsync(
                key,
                cancellationToken);

            // if value is not present in cache
            if (string.IsNullOrEmpty(cachedUrl))
            {
                ShortenedUrl? url = await _decorated.GetByCodeAsync(code, cancellationToken);

                if (url is null)
                {
                    return url;
                }

                // cache url in Redis
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(url),
                    cancellationToken);

                return url;
            }

            var urlFromCache = JsonConvert.DeserializeObject<ShortenedUrl>(
                cachedUrl,
                new JsonSerializerSettings()
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                });

            if (urlFromCache is not null)
            {
                _dbContext.Set<ShortenedUrl>().Attach(urlFromCache);
            }

            return urlFromCache;
        }

        public async Task<ShortenedUrl?> GetByLongUrlAsync(string longUrl, CancellationToken cancellationToken = default)
        {
            string key = $"url-{longUrl}";

            // get value from cache
            string? cachedUrl = await _distributedCache.GetStringAsync(
                key,
                cancellationToken);

            // if value is not present in cache
            if (string.IsNullOrEmpty(cachedUrl))
            {
                ShortenedUrl? url = await _decorated.GetByLongUrlAsync(longUrl, cancellationToken);

                if (url is null)
                {
                    return url;
                }

                // cache url in Redis
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(url),
                    cancellationToken);

                return url;
            }

            var urlFromCache = JsonConvert.DeserializeObject<ShortenedUrl>(
                cachedUrl,
                new JsonSerializerSettings()
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver()
                });

            if (urlFromCache is not null)
            {
                _dbContext.Set<ShortenedUrl>().Attach(urlFromCache);
            }

            return urlFromCache;
        }
    }
}
