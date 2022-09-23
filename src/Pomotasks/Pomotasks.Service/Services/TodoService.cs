using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Interfaces;
using Pomotasks.Persistence.Interfaces;
using Pomotasks.Service.Interfaces;
using System.Linq.Expressions;

namespace Pomotasks.Service.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper<Todo, DtoTodo> _mapper;

        public TodoService(ITodoRepository repository, IMapper<Todo, DtoTodo> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoTodo>> FindAll()
        {
            try
            {
                var todos = await _repository.FindAll();

                if (todos is null)
                {
                    return null;
                }

                return _mapper.GetDtos(todos);
            }
            catch (Exception ex)
            {
                string message = "Could not find tasks";

                throw new Exception(message, ex);
            }
        }

        public async Task<IEnumerable<DtoTodo>> FindBy(Expression<Func<Todo, bool>> filter)
        {
            try
            {
                var todos = await _repository.FindBy(filter);

                if (todos is null)
                {
                    return null;
                }

                return _mapper.GetDtos(todos);
            }
            catch (Exception ex)
            {
                string message = "Could not find tasks";

                throw new Exception(message, ex);
            }
        }

        public async Task<DtoTodo> FindById(Guid id)
        {
            try
            {
                var todo = await _repository.FindById(id);

                if (todo is null)
                {
                    return null;
                }

                return _mapper.GetDto(todo);
            }
            catch (Exception ex)
            {
                string message = string.Format("Could not find task {0}", id);

                throw new Exception(message, ex);
            }
        }

        public async Task<DtoTodo> Add(DtoTodo dtoTodo)
        {
            try
            {
                var todo = _mapper.GetEntity(dtoTodo);

                if (todo is not null)
                {
                    _repository.Add(todo);

                    if (await _repository.SaveChangesAsync())
                    {
                        todo = await _repository.FindById(todo.Id);

                        return _mapper.GetDto(todo);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                string message = string.Format("It was not possible to add the task {0}", dtoTodo.Title);

                throw new Exception(message, ex);
            }
        }

        public async Task<DtoTodo> Update(DtoTodo dtoTodo)
        {
            try
            {
                var id = Guid.Parse(dtoTodo.Id);
                var todo = await _repository.FindById(id);

                if (todo is not null)
                {
                    todo = _mapper.GetEntity(dtoTodo);

                    _repository.Update(todo);

                    if (await _repository.SaveChangesAsync())
                    {
                        todo = await _repository.FindById(id);

                        return _mapper.GetDto(todo);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                string message = string.Format("It was not possible to update the task {0}", dtoTodo.Title);

                throw new Exception(message, ex);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var todo = await _repository.FindById(id);

                if (todo is null)
                {
                    throw new Exception("Task to delete not found.");
                }

                _repository.Delete(todo);

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete task.", ex);
            }
        }

        public async Task<bool> DeleteRange(List<Guid> ids)
        {
            try
            {
                var todos = await _repository.FindBy(todo => ids.Any(id => id == todo.Id));

                if (todos is null)
                {
                    throw new Exception("Tasks to delete not found.");
                }

                if (todos.Count() == ids.Count)
                {
                    _repository.DeleteRange(todos);
                }

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete tasks.", ex);
            }
        }
    }
}
