using Pomotasks.Domain.Interfaces;
using Pomotasks.Persistence.Context;
using Pomotasks.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Persistence.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
