using Pomotasks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Persistence.Interfaces
{
    public interface IRepository<T> where T : IEntityBase
    {
        Task<T> GetById(Guid id);

        Task<List<T>> GetBy(Expression<Func<T, bool>> filter);

        Task Add(T entity);

        Task Update(T entity);

        Task<bool> Delete(T entity);

        Task<bool> SaveChangesAsync();
    }
}
