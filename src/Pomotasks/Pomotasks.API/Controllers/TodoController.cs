using Microsoft.AspNetCore.Mvc;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Globalization;
using Pomotasks.Service.Interfaces;
using System.Net;

namespace Pomotasks.API.Controllers
{
    [Route(template: "api/v1/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private const int DEFAULT_SKIP = 0;
        private const int DEFAULT_TAKE = 10;
        private const int LIMIT_TAKE = 30;
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet(template: "{userId}/{skip:int}/{take:int}")]
        public async Task<IActionResult> FindAllByUser(
            [FromRoute] string userId,
            [FromRoute] int skip = DEFAULT_SKIP,
            [FromRoute] int take = DEFAULT_TAKE)
        {
            try
            {
                if (take > LIMIT_TAKE)
                {
                    string message = string.Format(Message.GetMessage("21"), LIMIT_TAKE);

                    return BadRequest(message);
                }

                var dtoTodos = await _service.FindAll(userId, skip, take);

                if (dtoTodos is null)
                {
                    var message = string.Format(Message.GetMessage("24"), userId);

                    return BadRequest(message);
                }

                return Ok(ConvertToPaginatedResult(userId, skip, take, dtoTodos));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            try
            {
                var dtoTodo = await _service.FindById(id);

                if (dtoTodo is null)
                {
                    string message = string.Format(Message.GetMessage("1"), id);

                    return NotFound(message);
                }

                return Ok(dtoTodo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DtoTodo dtoTodo)
        {
            try
            {
                dtoTodo.Id = Guid.NewGuid().ToString();

                var result = await _service.Add(dtoTodo);

                if (result is null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DtoTodo dtoTodo)
        {
            try
            {
                if (dtoTodo is null)
                {
                    return BadRequest(Message.GetMessage("22"));
                }

                var result = await _service.Update(dtoTodo);

                if (result is null)
                {
                    return BadRequest(Message.GetMessage("23"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);

                if (!result)
                {
                    return BadRequest(Message.GetMessage("10"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetMessage(string key, string component)
        {
            return Ok(Message.GetMessage(key, new string[] { component }));
        }

        private DtoPaged<DtoTodo> ConvertToPaginatedResult(string userId, int skip, int take, IEnumerable<DtoTodo> dtoTodos)
        {
            var totalCount = _service.GetTotalCount(userId);
            var currentPage = skip < take ? 1 : ((skip / take) + 1);

            return new DtoPaged<DtoTodo>
            {
                CurrentPage = currentPage,
                Skip = skip,
                Take = take,
                TotalCount = totalCount.Result,
                Data = dtoTodos
            };
        }
    }
}
