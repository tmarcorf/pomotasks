using Pomotasks.Domain.Entities;
using System.Linq.Expressions;

namespace Pomotasks.Persistence.Interfaces
{
    public interface ITodoRepository : IFindRepository<Todo>, IRepository<Todo>
    {
        Task<int> GetTotalCountBy(Expression<Func<Todo, bool>> filter);
    }
}
