using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public record DepartmentDto(Guid Id, string Name, string Code);
public record CretaeDtoDepartment(Guid Id, string Name, string Code);

public class DepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public static DepartmentDto MapToDto(Department department) => new(
        department.Id,
        department.Name,
        department.Code
        );

    public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync(CancellationToken cancellation = default)
    {
        var departments = await _repository.GetAllAsync(cancellation);
        return departments.Select(MapToDto);
    }

    public async Task<DepartmentDto?> GetDepartmentByIdAsync(Guid id, CancellationToken cancellation = default)
    {
        var department = await _repository.GetByIdAsync(id, cancellation);
        if (department == null) return null;

        return MapToDto(department);
    }

    public async Task<Guid> CreateDepartmentAsync(CretaeDtoDepartment dto, CancellationToken cancellation = default)
    {
        var department = new Department(dto.Id, dto.Name, dto.Code);

        await _repository.AddAsync(department);
        return department.Id;
    }
}
