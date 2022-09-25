using Microsoft.AspNetCore.Mvc;
using Pomotasks.Domain.Dtos;
using Pomotasks.Service.Interfaces;

namespace Pomotasks.API.Controllers
{
    [Route("api/v1/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var dtoTodos = await _service.FindAll();

                if (dtoTodos is null)
                {
                    return BadRequest();
                }

                return Ok(dtoTodos);
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
