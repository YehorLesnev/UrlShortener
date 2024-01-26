

using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Repositories.CachedRepositoryDecorators;
using UrlShortener.Repositories.Implementations;
using UrlShortener.Repositories.Interfaces;
using UrlShortener.Services;

namespace UrlShortener
{
    public static class ServicesExtensionMethods
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ShortenedUrlRepository>();
            services.AddScoped<IShortenedUrlRepository, CachedShortenedUrlRepository>();
            services.AddScoped<IMemoryCache, MemoryCache>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UrlShorteningService>();
        }
    }
}