using System.Linq.Expressions;

namespace UrlShortener.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public IQueryable<T> GetAll(Expression<Func<T, bool>> conditions);
        public IQueryable<T> GetAllWith(Expression<Func<T, bool>> conditions, params Expression<Func<T, object>>[] includeExpressions);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> conditions);
        public bool Any(Expression<Func<T, bool>> conditions);
        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public T? GetById(Guid id);
        public void Insert(T record);
        public void InsertRange(IEnumerable<T> records);
        public void Update(T record);
        public void UpdateRange(IEnumerable<T> records);
        public void Delete(T record);
        public void DeleteIfNotNull(T? record);
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        public void DeleteRange(IEnumerable<T> entities);
        public Task CommitAsync();
        public void Commit();
    }
}
