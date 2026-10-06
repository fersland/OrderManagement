using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public record DeveloperDto(int Id, string Name, int Age);
public record CreateDeveloperDto(string Name, int Age);
public record UpdateDeveloperDto(string Name, int Age);

public class DeveloperService
{
    private readonly IDeveloperRepository _repository;

    public DeveloperService(IDeveloperRepository repository)
    {
        _repository = repository;
    }

    public static DeveloperDto MapToDto(Developer developer) => new(
        developer.Id,
        developer.Name,
        developer.Age
        );

    public async Task<IEnumerable<DeveloperDto>> GetAllDeveloperAsync(CancellationToken cancellation = default)
    {
        var dev = await _repository.GetAllAsync(cancellation);
        return dev.Select(MapToDto);
    }

    public async Task<DeveloperDto?> GetByIdDeveloperAsync(int id, CancellationToken cancellation = default)
    {
        var dev = await _repository.GetByIdAsync(id, cancellation);
        if (dev == null) return null;

        return MapToDto(dev);
    }

    public async Task<int> CreateDeveloperAsync(CreateDeveloperDto dto, CancellationToken cancellation = default)
    {
        var dev = new Developer(dto.Name, dto.Age);
        await _repository.AddAsync(dev, cancellation);
        return dev.Id;
    }

    public async Task UpdateDeveloperAsync(int id, UpdateDeveloperDto dto, CancellationToken cancellation = default)
    {
        var dev = await _repository.GetByIdAsync(id, cancellation);

        if (dev == null) throw new KeyNotFoundException($"No se encontro el desarrollador");

        dev.UpdateDetails(dto.Name, dto.Age);
        await _repository.UpdateAsync(dev, cancellation);
    }

    public  async Task DeleteDeveloperAsync(int id, CancellationToken cancellation = default)
    {
        var dev = await _repository.GetByIdAsync(id, cancellation);
        if (dev == null) throw new KeyNotFoundException($"No se encontro al desarrollador");

        await _repository.DeleteAsync(id);
    }

}
