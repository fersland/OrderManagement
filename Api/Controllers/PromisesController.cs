using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromisesController : Controller
    {
        private readonly PromiseService _service;

        public PromisesController(PromiseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellation = default)
        {
            try
            {
                var data = await _service.GetAllPromisesAsync(cancellation);

                if (data == null || !data.Any()) return NotFound(new { message = "No se encontraron datos." });

                return Ok(data);
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error en el servidor.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation = default)
        {
            try
            {
                var data = await _service.GetByIdPromise(id, cancellation);

                if (data == null) return NotFound(new { message = "No se encontraron datos." });
                return Ok(data);
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error en el servidor.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CreatePromiseDto dto,
            [FromServices] IValidator<CreatePromiseDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validateResult = await validator.ValidateAsync(dto, cancellation);
                if (validateResult.IsValid == false) return BadRequest(validateResult.ToDictionary());
                var dataId = await _service.CreatePromiseAsync(dto, cancellation);

                return CreatedAtAction(nameof(GetById), new { id = dataId }, new { id = dataId });
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error en el servidor",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromiseDto dto,
            [FromServices] IValidator<UpdatePromiseDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validateResult = await validator.ValidateAsync(dto, cancellation);

                if (!validateResult.IsValid) return BadRequest(validateResult.ToDictionary());

                await _service.UpdatePromiseAsync(id, dto, cancellation);
                return NoContent();

            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error en el servidor",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation = default)
        {
            try
            {
                var data = await _service.GetByIdPromise(id, cancellation);
                if (data == null) return NotFound(new { message = "No se encontro el dato." });

                await _service.DeletePromiseAsync(id, cancellation);
                return NoContent();
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error en el servidor",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }
    }
}
