using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public record PromiseDto(int Id, string Name, int Age, string Colour);
public record CreatePromiseDto(string Name, int Age, string Colour);
public record UpdatePromiseDto(int Id, string Name, int Age, string Colour);

public class PromiseService
{
    private readonly IPromiseRepository _repository;

    public PromiseService(IPromiseRepository repository)
    {
        _repository = repository;
    }

    public static PromiseDto MapToDto(Promise promise) => new (promise.Id, promise.Name, promise.Age, promise.Colour);

    public async Task<IEnumerable<PromiseDto>> GetAllPromisesAsync(CancellationToken cancellation = default)
    {
        var promise = await _repository.GetAllAsync(cancellation);
        return promise.Select(MapToDto);
    }

    public async Task<PromiseDto?> GetByIdPromise(int id, CancellationToken cancellation = default)
    {
        var data = await _repository.GetByIdAsync(id, cancellation);
        if (data == null) return null;

        return MapToDto(data);
    }

    public async Task<int> CreatePromiseAsync(CreatePromiseDto dto, CancellationToken cancellation = default)
    {
        var data = new Promise(dto.Name, dto.Age, dto.Colour);
        await _repository.AddAsync(data);
        return data.Id;
    }

    public async Task UpdatePromiseAsync(int id, UpdatePromiseDto dto, CancellationToken cancellation = default)
    {
        var data = await _repository.GetByIdAsync(id, cancellation);

        if (data == null) throw new KeyNotFoundException($"No se encontro el dato.");

        data.UpdateDetails(dto.Name, dto.Age, dto.Colour);
        await _repository.UpdateAsync(data);
    }

    public async Task DeletePromiseAsync(int id, CancellationToken cancellation = default)
    {
        var data = _repository.GetByIdAsync(id, cancellation);

        if (data == null) throw new KeyNotFoundException($"No se encontro el dato.");

        await _repository.DeleteAsync(id);
    }
}
