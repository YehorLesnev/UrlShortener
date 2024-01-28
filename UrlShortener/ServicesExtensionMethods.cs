

using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Repositories.CachedRepositoryDecorators;
using UrlShortener.Repositories.Implementations;
using UrlShortener.Repositories.Interfaces;
using UrlShortener.Services;
using UrlShortener.Services.Static;

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
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UrlShorteningService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
        }
    }
}