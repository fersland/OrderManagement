using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DepartmentsController : Controller
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _service.GetAllDepartmentAsync();
        return Ok(departments);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var department = await _service.GetDepartmentByIdAsync(id);
        if (department == null) return NotFound("Departamento no encontrado.");

        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] CretaeDtoDepartment dto, [FromServices] IValidator<CretaeDtoDepartment> validator)
    {
        try
        {
            var validateResult = await validator.ValidateAsync(dto);
            if(validateResult != null)
            {
                return BadRequest(validateResult.ToDictionary());
            }

            var departmentId = await _service.CreateDepartmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = departmentId }, new { id = dto.Id });

        }
        catch (Exception ex)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error interno del servidor.",
                Detail = ex.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problem);
        }
    }

}
