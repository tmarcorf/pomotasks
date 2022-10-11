using FluentValidation.Results;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Globalization;
using Pomotasks.Domain.Interfaces;
using Pomotasks.Domain.Validations;
using Pomotasks.Persistence.Interfaces;
using Pomotasks.Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Pomotasks.Service.Services
{
    public class TodoService : ServiceBase<DtoTodo, TodoValidator>, ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapping<Todo, DtoTodo> _mapper;

        public TodoService(ITodoRepository repository, IMapping<Todo, DtoTodo> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DtoPaged<DtoTodo>> FindAll(string userId, int skip, int take)
        {
            try
            {
                var userIdAsGuid = GetIdAsGuid(userId);
                var todos = await _repository.FindAll(userIdAsGuid, skip, take);

                if (todos is null || todos.Count() == 0)
                {
                    return null;
                }

                var totalCount = GetTotalCount(userId);
                var currentPage = skip < take ? 1 : ((skip / take) + 1);

                return ConvertToPaginatedResult(skip, take, totalCount.Result, _mapper.GetDtos(todos));
            }
            catch (Exception ex)
            {
                string message = Message.GetMessage("2");

                throw new Exception(message, ex);
            }
        }

        public async Task<IEnumerable<DtoTodo>> FindBy(Expression<Func<Todo, bool>> filter)
        {
            try
            {
                var todos = await _repository.FindBy(filter);

                if (todos is null || todos.Count() == 0)
                {
                    return null;
                }

                return _mapper.GetDtos(todos);
            }
            catch (Exception ex)
            {
                string message = Message.GetMessage("2");

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
                string message = string.Format(Message.GetMessage("1"), id);

                throw new Exception(message, ex);
            }
        }

        public async Task<int> GetTotalCount(string userId)
        {
            try
            {
                var userIdAsGuid = GetIdAsGuid(userId);

                return await _repository.GetTotalCount(userIdAsGuid);
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("3"), userId);

                throw new Exception(message, ex);
            }
        }

        public async Task<DtoTodo> Add(DtoTodo dtoTodo)
        {
            try
            {
                dtoTodo.CreationDate = DateTime.Now;

                Validate(dtoTodo);
                
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
                string message = string.Format(Message.GetMessage("9"), dtoTodo.Title);

                throw new Exception(message, ex);
            }
        }

        public async Task<DtoTodo> Update(DtoTodo dtoTodo)
        {
            try
            {
                var id = Guid.Parse(dtoTodo.Id);
                var todo = await _repository.FindById(id);

                Validate(dtoTodo);

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
                string message = string.Format(Message.GetMessage("11"), dtoTodo.Title);

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
                    throw new Exception(Message.GetMessage("12"));
                }

                _repository.Delete(todo);

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("10"), id);

                throw new Exception(message, ex);
            }
        }

        public async Task<bool> DeleteRange(List<string> ids)
        {
            try
            {
                var guids = GetIdsAsGuid(ids); 
                var todos = await _repository.FindBy(todo => guids.Any(guid => guid == todo.Id));

                if (todos is null || todos.Count() == 0)
                {
                    throw new Exception(Message.GetMessage("18"));
                }

                if (todos.Count() == ids.Count)
                {
                    _repository.DeleteRange(todos);
                }

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(Message.GetMessage("19"), ex);
            }
        }
    }
}
