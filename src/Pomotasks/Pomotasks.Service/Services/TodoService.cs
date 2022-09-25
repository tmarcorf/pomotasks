﻿using Pomotasks.Domain.Dtos;
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
        private readonly IMapping<Todo, DtoTodo> _mapper;

        public TodoService(ITodoRepository repository, IMapping<Todo, DtoTodo> mapper)
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

        public async Task<DtoTodo> FindById(string id)
        {
            try
            {
                var guid = GetIdAsGuid(id);
                var todo = await _repository.FindById(guid);

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
                dtoTodo.CreationDate = DateTime.Now;
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

        public async Task<bool> Delete(string id)
        {
            try
            {
                var guid = GetIdAsGuid(id);
                var todo = await _repository.FindById(guid);

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

        public async Task<bool> DeleteRange(List<string> ids)
        {
            try
            {
                var guids = GetIdsAsGuid(ids); 
                var todos = await _repository.FindBy(todo => guids.Any(guid => guid == todo.Id));

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

        private Guid GetIdAsGuid(string id)
        {
            Guid result;

            if (Guid.TryParse(id, out result))
            {
                return result;
            }

            return Guid.Empty;
        }

        private List<Guid> GetIdsAsGuid(List<string> ids)
        {
            List<Guid> guids = new List<Guid>();

            ids.ForEach(id =>
            {
                Guid result;
                if (Guid.TryParse(id, out result))
                {
                    guids.Add(result);
                }
            });

            return guids;
        }
    }
}
