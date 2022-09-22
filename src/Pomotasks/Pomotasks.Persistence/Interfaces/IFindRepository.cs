using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Persistence.Interfaces
{
    public interface IFindRepository<T> where T : class
    {
        Task<T> FindById(Guid id);

        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> FindAll();
    }
}
