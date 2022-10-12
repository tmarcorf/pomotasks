using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Globalization;
using Pomotasks.Domain.Interfaces;
using Pomotasks.Domain.Validations;
using Pomotasks.Persistence.Interfaces;
using Pomotasks.Service.Interfaces;
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

        public async Task<DtoPagedResult<DtoTodo>> FindAll(string userId, int skip, int take)
        {
            DtoPagedResult<DtoTodo> result = new();
            int totalCount;
            var userIdAsGuid = GetIdAsGuid(userId);
            var errorMessages = new List<string>();

            try
            {
                var todos = await _repository.FindAll(userIdAsGuid, skip, take);

                if (todos is null || todos.Count() == 0)
                {
                    totalCount = 0;
                    errorMessages.Add(string.Format(Message.GetMessage("24"), userId));

                    ConfigurePagedResult(result, skip, take, totalCount, new List<DtoTodo>(), errorMessages);

                    return result;
                }

                totalCount = GetTotalCountBy(x => x.UserId == userIdAsGuid).Result;

                ConfigurePagedResult(result, skip, take, totalCount, _mapper.GetDtos(todos));

                return result;
            }
            catch (Exception ex)
            {
                errorMessages.Clear();

                errorMessages.Add(Message.GetMessage("2"));
                errorMessages.Add(ex.Message);

                ConfigurePagedResult(result, skip, take, 0, new List<DtoTodo>(), errorMessages);

                return result;
            }
        }

        public async Task<DtoPagedResult<DtoTodo>> FindBy(Expression<Func<Todo, bool>> filter, int skip, int take)
        {
            DtoPagedResult<DtoTodo> result = new();
            int totalCount;
            var errorMessages = new List<string>();

            try
            {
                var todos = await _repository.FindBy(filter, skip, take);

                if (todos is null || todos.Count() == 0)
                {
                    totalCount = 0;
                    errorMessages.Add(string.Format(Message.GetMessage("25")));

                    ConfigurePagedResult(result, skip, take, totalCount, new List<DtoTodo>(), errorMessages);

                    return result;
                }

                ConfigurePagedResult(result, skip, take, todos.Count(), _mapper.GetDtos(todos));

                return result;
            }
            catch (Exception ex)
            {
                errorMessages.Clear();

                errorMessages.Add(Message.GetMessage("2"));
                errorMessages.Add(ex.Message);

                ConfigurePagedResult(result, skip, take, 0, new List<DtoTodo>(), errorMessages);

                return result;
            }
        }

        public async Task<DtoSingleResult<DtoTodo>> FindById(string id)
        {
            DtoSingleResult<DtoTodo> singleResult = new();
            var errorMessages = new List<string>();

            try
            {
                var guid = GetIdAsGuid(id);
                var todo = await _repository.FindById(guid);

                if (todo is null)
                {
                    var message = string.Format(Message.GetMessage("26"), id);
                    errorMessages.Add(message);

                    ConfigureSingleResult(singleResult, null, errorMessages);

                    return singleResult;
                }

                ConfigureSingleResult(singleResult, _mapper.GetDto(todo));

                return singleResult;
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("1"), id);

                errorMessages.Clear();
                errorMessages.Add(message);
                errorMessages.Add(ex.Message);

                ConfigureSingleResult(singleResult, null, errorMessages);

                return singleResult;
            }
        }

        public async Task<DtoSingleResult<DtoTodo>> Add(DtoTodo dtoTodo)
        {
            DtoSingleResult<DtoTodo> singleResult = new();
            var errorMessages = new List<string>();

            try
            {
                dtoTodo.CreationDate = DateTime.Now;

                var validationResult = Validate(dtoTodo);

                if (!validationResult.IsValid)
                {
                    ConfigureSingleResult(singleResult, null, GetErrors(validationResult.Errors));

                    return singleResult;
                }

                var todo = _mapper.GetEntity(dtoTodo);

                if (todo is not null)
                {
                    _repository.Add(todo);

                    if (await _repository.SaveChangesAsync())
                    {
                        ConfigureSingleResult(singleResult, dtoTodo);
                    }
                }

                return singleResult;
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("9"), dtoTodo.Title);

                errorMessages.Clear();
                errorMessages.Add(message);
                errorMessages.Add(ex.Message);

                ConfigureSingleResult(singleResult, null, errorMessages);

                return singleResult;
            }
        }

        public async Task<DtoSingleResult<DtoTodo>> Update(DtoTodo dtoTodo)
        {
            DtoSingleResult<DtoTodo> singleResult = new();
            var errorMessages = new List<string>();

            try
            {
                var validationResult = Validate(dtoTodo);

                if (!validationResult.IsValid)
                {
                    ConfigureSingleResult(singleResult, null, GetErrors(validationResult.Errors));

                    return singleResult;
                }

                var id = Guid.Parse(dtoTodo.Id);

                if (await _repository.AnyAsync(x => x.Id == id))
                {
                    var todo = _mapper.GetEntity(dtoTodo);

                    _repository.Update(todo);

                    if (await _repository.SaveChangesAsync())
                    {
                        ConfigureSingleResult(singleResult, dtoTodo);
                    }
                }

                return singleResult;
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("11"), dtoTodo.Title);

                errorMessages.Clear();
                errorMessages.Add(message);
                errorMessages.Add(ex.Message);

                ConfigureSingleResult(singleResult, null, errorMessages);

                return singleResult;
            }
        }

        public async Task<DtoResult> Delete(string id)
        {
            DtoResult result = new();
            var errorMessages = new List<string>();

            try
            {
                var idAsGuid = GetIdAsGuid(id);

                if (!await _repository.AnyAsync(x => x.Id == idAsGuid))
                {
                    var message = string.Format(Message.GetMessage("12"), id);

                    errorMessages.Add(message);
                    ConfigureResult(result, errorMessages);

                    return result;
                }

                var todo = await _repository.FindById(idAsGuid);

                _repository.Delete(todo);

                if (await _repository.SaveChangesAsync())
                {
                    ConfigureResult(result);
                }

                return result;
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("10"), id);

                errorMessages.Clear();
                errorMessages.Add(message);
                errorMessages.Add(ex.Message);

                ConfigureResult(result, errorMessages);

                return result;
            }
        }

        public async Task<DtoResult> DeleteRange(List<string> ids)
        {
            DtoResult result = new();
            var errorMessages = new List<string>();

            try
            {
                var idsAsGuid = GetIdsAsGuid(ids);
                var exist = await _repository.AnyAsync(todo => idsAsGuid.Any(id => id == todo.Id));

                if (!exist)
                {
                    errorMessages.Add(Message.GetMessage("18"));
                    ConfigureResult(result, errorMessages);

                    return result;
                }

                var totalCount = GetTotalCountBy(todo => idsAsGuid.Any(id => id == todo.Id)).Result;

                if (totalCount == ids.Count)
                {
                    var todos = await _repository.FindBy(todo => idsAsGuid.Any(id => id == todo.Id));

                    _repository.DeleteRange(todos);

                    if (await _repository.SaveChangesAsync())
                    {
                        ConfigureResult(result);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                string message = Message.GetMessage("19");

                errorMessages.Add(message);
                errorMessages.Add(ex.Message);

                ConfigureResult(result, errorMessages);

                return result;
            }
        }

        #region PRIVATE METHODS

        private async Task<int> GetTotalCountBy(Expression<Func<Todo, bool>> filter)
        {
            try
            {
                return await _repository.GetTotalCountBy(filter);
            }
            catch (Exception ex)
            {
                string message = string.Format(Message.GetMessage("3"));

                throw new Exception(message, ex);
            }
        }

        #endregion
    }
}
