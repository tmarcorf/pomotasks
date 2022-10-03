using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using System.Linq.Expressions;

namespace Pomotasks.Service.Interfaces
{
    public interface ITodoService
    {
        Task<DtoTodo> FindById(string id);

        Task<IEnumerable<DtoTodo>> FindBy(Expression<Func<Todo, bool>> filter);

        Task<IEnumerable<DtoTodo>> FindAll(string userId, int skip, int take);

        Task<int> GetTotalCount(string userId);

        Task<DtoTodo> Add(DtoTodo dtoTodo);

        Task<DtoTodo> Update(DtoTodo dtoTodo);

        Task<bool> Delete(string id);

        Task<bool> DeleteRange(List<string> ids);
    }
}
