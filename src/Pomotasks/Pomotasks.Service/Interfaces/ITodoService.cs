using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Service.Interfaces
{
    public interface ITodoService
    {
        Task<DtoTodo> FindById(Guid id);

        Task<IEnumerable<DtoTodo>> FindBy(Expression<Func<DtoTodo, bool>> filter);

        Task<IEnumerable<DtoTodo>> FindAll();

        Task<DtoTodo> Add(DtoTodo todo);

        Task<DtoTodo> Update(DtoTodo todo);

        Task<bool> Delete(Guid id);

        Task<bool> DeleteRange(IEnumerable<Guid> todo);
    }
}
