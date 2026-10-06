using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        private readonly PetService _service;

        public PetsController(PetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pets = await _service.GetAllPetsAsync();
                if (pets == null) return NotFound(new { message = "No se encontraron nombres de mascotas." });

                return Ok(pets);
            }
            catch (Exception ex)
            {
                var errors = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Erro interno del servidor.",
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
                var pets = await _service.GetByIdPetAsync(id);
                if (pets == null) return NotFound(new { messgae = "No se encontro ninguna coincidencia." });

                return Ok(pets);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetsDto dto,
            [FromServices] IValidator<CreatePetsDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validateResult = await validator.ValidateAsync(dto, cancellation);
                if (!validateResult.IsValid) return BadRequest(validateResult.ToDictionary());

                var petsId = await _service.CreatePetAsync(dto, cancellation);
                return CreatedAtAction(nameof(GetById), new { id = petsId }, new { id = petsId });
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePetsDto dto,
            [FromServices] IValidator<UpdatePetsDto> validator,
            CancellationToken cancellation = default)
        {
            try
            {
                var validateResult = await validator.ValidateAsync(dto, cancellation);
                if (!validateResult.IsValid) return BadRequest(validateResult.ToDictionary());

                await _service.UpdatePetAsync(id, dto, cancellation);
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
