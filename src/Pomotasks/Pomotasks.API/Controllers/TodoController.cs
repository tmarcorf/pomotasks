using Microsoft.AspNetCore.Mvc;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Globalization;
using Pomotasks.Service.Interfaces;

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

                var pagedResult = await _service.FindAll(userId, skip, take);

                if (!pagedResult.Success)
                {
                    return BadRequest(pagedResult);
                }

                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            try
            {
                var singleResult = await _service.FindById(id);

                if (!singleResult.Success)
                {
                    return NotFound(singleResult);
                }

                return Ok(singleResult);
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
                if (dtoTodo is null)
                {
                    return BadRequest(Message.GetMessage("22"));
                }

                dtoTodo.Id = Guid.NewGuid().ToString();

                var singleResult = await _service.Add(dtoTodo);

                if (!singleResult.Success)
                {
                    return BadRequest(singleResult);
                }

                return Ok(singleResult);
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

                var singleResult = await _service.Update(dtoTodo);

                if (!singleResult.Success)
                {
                    return BadRequest(singleResult);
                }

                return Ok(singleResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("ids")]
        public async Task<IActionResult> DeleteRange([FromBody] List<string> ids)
        {
            try
            {
                var result = await _service.DeleteRange(ids);

                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
