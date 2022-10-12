using System.Linq.Expressions;

namespace Pomotasks.Persistence.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task<bool> SaveChangesAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}
