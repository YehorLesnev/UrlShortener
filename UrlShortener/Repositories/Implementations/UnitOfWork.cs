using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Repositories.Implementations
{
    public class UnitOfWork(
        ApplicationDbContext context,
        IShortenedUrlRepository? shortenedUrlRepository)
        : IUnitOfWork
    {
        public IShortenedUrlRepository ShortenedUrlRepository => shortenedUrlRepository ??= new ShortenedUrlRepository(context);

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public UnitOfWork(ApplicationDbContext context) : this(context, null)
        {
        }
    }
}
