using Pomotasks.Domain.Entities;

namespace Pomotasks.Persistence.Interfaces
{
    public interface ITodoRepository : IFindRepository<Todo>, IRepository<Todo>
    {
        Task<int> GetTotalCount(Guid userId);
    }
}
