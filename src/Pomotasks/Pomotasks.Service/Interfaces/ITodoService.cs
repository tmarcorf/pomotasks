using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using System.Linq.Expressions;

namespace Pomotasks.Service.Interfaces
{
    public interface ITodoService
    {
        Task<DtoSingleResult<DtoTodo>> FindById(string id);

        Task<DtoPagedResult<DtoTodo>> FindBy(Expression<Func<Todo, bool>> filter, int skip, int take);

        Task<DtoPagedResult<DtoTodo>> FindAll(string userId, int skip, int take);

        Task<DtoSingleResult<DtoTodo>> Add(DtoTodo dtoTodo);

        Task<DtoSingleResult<DtoTodo>> Update(DtoTodo dtoTodo);

        Task<DtoResult> Delete(string id);

        Task<DtoResult> DeleteRange(List<string> ids);
    }
}
