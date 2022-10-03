using Microsoft.EntityFrameworkCore;
using Pomotasks.Domain.Entities;
using Pomotasks.Persistence.Context;
using Pomotasks.Persistence.Interfaces;
using System.Linq.Expressions;

namespace Pomotasks.Persistence.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(ApplicationContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Todo>> FindAll(Guid userId, int skip, int take)
        {
            IQueryable<Todo> query = _context.Todos;

            query = query
                .AsNoTracking()
                .Where(todo => todo.UserId == userId)
                .Skip(skip)
                .Take(take)
                .OrderBy(todo => todo.CreationDate);

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<Todo>> FindBy(Expression<Func<Todo, bool>> filter)
        {
            IQueryable<Todo> query = _context.Todos;

            query = query
                .AsNoTracking()
                .Where(filter)
                .OrderBy(todo => todo.CreationDate);

            return await query.ToArrayAsync();
        }

        public async Task<Todo> FindById(Guid id)
        {
            IQueryable<Todo> query = _context.Todos;

            query = query
                .AsNoTracking()
                .Where(todo => todo.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalCount(Guid userId)
        {
            IQueryable<Todo> query = _context.Todos;

            query = query
                .AsNoTracking()
                .Where(todo => todo.UserId == userId);

            return await query.CountAsync();
        }
    }
}
