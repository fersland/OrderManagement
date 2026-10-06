using Application.Common.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : Controller
    {
        private readonly DeveloperService _service;

        public DevelopersController(DeveloperService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await _service.GetAllDeveloperAsync();

                if(data == null)  return NotFound(new { message = "No se encontraron datos" });

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
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdDeveloperAsync(id);
                if (result == null) return NotFound(new { message = "No se encontro el desarrollador." });

                return Ok(result);
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
        public async Task<IActionResult> Create([FromBody] CreateDeveloperDto dto,
            [FromServices] IValidator<CreateDeveloperDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validateResult = await validator.ValidateAsync(dto, cancellation);

                if (validateResult.IsValid == false) return BadRequest(validateResult.ToDictionary());

                var developerId = await _service.CreateDeveloperAsync(dto, cancellation);

                return CreatedAtAction(nameof(GetById), new { id = developerId }, new { id = developerId });
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails {
                    Title = "Error en el servidor.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDeveloperDto dto,
            [FromServices] IValidator<UpdateDeveloperDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validateResult = await validator.ValidateAsync(dto, cancellation);
                if (validateResult.IsValid == false) return BadRequest(new { message = "Veirfique la validaciones del desarrollador." });

                await _service.UpdateDeveloperAsync(id, dto, cancellation);
                return NoContent();

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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation = default)
        {
            try
            {
                var idem = await _service.GetByIdDeveloperAsync(id);
                if (idem == null) return NotFound(new { message = "No se encontro el desarrollador." });

                await _service.DeleteDeveloperAsync(id, cancellation);
                return NoContent();
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

    }
}
