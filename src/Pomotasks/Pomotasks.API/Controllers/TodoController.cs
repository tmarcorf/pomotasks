using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Pomotasks.Domain.Dtos;
using Pomotasks.Service.Interfaces;

namespace Pomotasks.API.Controllers
{
    [Route(template:"api/v1/todo")]
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

        [HttpGet(template:"{userId}/{skip:int}/{take:int}")]
        public async Task<IActionResult> FindAllByUser(
            [FromRoute] string userId,
            [FromRoute] int skip = DEFAULT_SKIP, 
            [FromRoute] int take = DEFAULT_TAKE)
        {
            try
            {
                if (take > LIMIT_TAKE)
                {
                    return BadRequest($"O limite de itens por página é de {LIMIT_TAKE}");
                }

                var dtoTodos = await _service.FindAll(userId, skip, take);

                if (dtoTodos is null)
                {
                    return BadRequest();
                }

                var totalCount = await _service.GetTotalCount(userId);
                var currentPage = skip < take ? 1 : ((skip / take) + 1);

                return Ok(new DtoPaged<DtoTodo>
                {
                    CurrentPage = currentPage,
                    Skip = skip,
                    Take = take,
                    TotalCount = totalCount,
                    Data = dtoTodos
                });
            }
            catch (Exception)
            {
                throw;
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
                    string message = string.Format("The requested id {0} was not found.", id);

                    return NotFound(message);
                }

                return Ok(dtoTodo);
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DtoTodo dtoTodo)
        {
            try
            {
                if (dtoTodo is null)
                {
                    return BadRequest("The ToDo information are required");
                }

                var result = await _service.Update(dtoTodo);

                if (result is null)
                {
                    return BadRequest("Some information are invalid.");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
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
                    return BadRequest("Could not delete.");
                }

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
