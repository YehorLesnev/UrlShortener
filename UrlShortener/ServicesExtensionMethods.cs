

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
            services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UrlShorteningService>();
        }
    }
}