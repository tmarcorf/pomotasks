using System.Linq.Expressions;

namespace Pomotasks.Persistence.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T FindById(Guid id);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> filter);

        IEnumerable<T> FindAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        Task<bool> SaveChangesAsync();
    }
}
