using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrlShortener.Repositories.Interfaces;

namespace UrlShortener.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _dbContext { get; set; }
        protected DbSet<T> MyDbSet { get; set; }

        public Repository(ApplicationDbContext context)
        {
            _dbContext = context;
            MyDbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll() => MyDbSet;

        public IQueryable<T> GetAll(Expression<Func<T, bool>> conditions)
        {
            return MyDbSet.Where(conditions);
        }

        public IQueryable<T> GetAllWith(Expression<Func<T, bool>> conditions, params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = GetAll();

            if (includeExpressions.Length == 0) return set.Where(conditions);

            set = includeExpressions.Aggregate(set, (current, includeExpression) => current.Include(includeExpression));

            return set.Where(conditions);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> conditions) => await MyDbSet.AnyAsync(conditions);

        public bool Any(Expression<Func<T, bool>> conditions) => MyDbSet.Any(conditions);

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await MyDbSet.FindAsync(id, cancellationToken);

        public T? GetById(Guid id) => MyDbSet.Find(id);

        public void Insert(T record)
        {
            var dbe = _dbContext.Entry(record);

            if (dbe.State != EntityState.Detached)
            {
                dbe.State = EntityState.Added;
            }
            else
            {
                MyDbSet.Add(record);
            }
        }

        public void InsertRange(IEnumerable<T> records)
        {
            MyDbSet.AddRange(records);
        }

        public void Update(T record)
        {
            var dbe = _dbContext.Entry(record);

            if (dbe.State == EntityState.Detached)
            {
                MyDbSet.Attach(record);
            }

            _dbContext.Entry(record).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> records)
        {
            MyDbSet.UpdateRange(records);
        }

        public void DeleteIfNotNull(T? record)
        {
            if (null != record)
            {
                _dbContext.Remove(record);
            }
        }

        public void Delete(T record)
        {
            _dbContext.Remove(record);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var record = await GetByIdAsync(id, cancellationToken);

            if (null == record) return false;

            _dbContext.Remove(record);

            return true;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            MyDbSet.RemoveRange(entities);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
