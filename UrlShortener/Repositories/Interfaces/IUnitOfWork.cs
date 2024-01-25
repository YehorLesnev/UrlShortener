namespace UrlShortener.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IShortenedUrlRepository ShortenedUrlRepository { get; }

        void Commit();
        Task CommitAsync();
    }
}
