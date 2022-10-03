using System.Linq.Expressions;

namespace Pomotasks.Persistence.Interfaces
{
    public interface IFindRepository<T> where T : class
    {
        Task<T> FindById(Guid id);

        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> FindAll(Guid userId, int skip, int take);
    }
}
