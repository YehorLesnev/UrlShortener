using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IShortenedUrlRepository? _shortenedUrlRepository;
        public IShortenedUrlRepository ShortenedUrlRepository => _shortenedUrlRepository ??= new ShortenedUrlRepository(_dbContext);

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public UnitOfWork(
            ApplicationDbContext context,
            IShortenedUrlRepository? shortenedUrlRepository)
        {
            _dbContext = context;
            _shortenedUrlRepository = shortenedUrlRepository;
        }
    }
}
