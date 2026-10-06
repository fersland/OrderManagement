using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FilmsController : Controller
    {
        private readonly FilmService _service;

        public FilmsController(FilmService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var films = await _service.GetAllFilmsAsync();

                if (films == null) return NotFound(new { message = "No se encontraron peliculas." });
                return Ok(films);
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error interno del servidor.",
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
                var films = await _service.GetByIdFilmsAsync(id);

                if (films == null) return NotFound(new { Message = $"No se encontro la pelicula." });

                return Ok(films);
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error interno del servidor.",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFilmsDto dto,
            [FromServices] IValidator<CreateFilmsDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(dto, cancellation);

                if(!validationResult.IsValid)
                {
                    return BadRequest(validationResult.ToDictionary());
                }

                var filmsId = await _service.CreateFilmsAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = filmsId }, new { id = filmsId });
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error interno del servidor.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,
            [FromBody] UpdateFilmDto dto, 
            [FromServices] IValidator<UpdateFilmDto> validator,
            CancellationToken cancellation)
        {
            try
            {
                var validationResult = await validator.ValidateAsync(dto, cancellation);

                if (!validationResult.IsValid) return BadRequest(validationResult.ToDictionary());

                await _service.UpdateFilmAsync(id, dto, cancellation);
                return NoContent();

            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "No se encontro la pelicula.",
                    Status = StatusCodes.Status404NotFound,
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Title = "Error interno del servidor.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }
    }
}
