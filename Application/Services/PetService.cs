using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public record PetsDto(int Id, string Name, string Colour, int Age);
public record CreatePetsDto(string Name, string Colour, int Age);
public record UpdatePetsDto(string Name, string Colour, int Age);


public class PetService
{
    private readonly IPetRepository _repository;

    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }

    public static PetsDto MapToDto(Pet pets) => new(
        pets.Id,
        pets.Name,
        pets.Colour,
        pets.Age
        );

    public async Task<IEnumerable<PetsDto>> GetAllPetsAsync(CancellationToken cancellation = default)
    {
        var pets = await _repository.GetAllAsync(cancellation);
        return pets.Select(MapToDto);
    }

    public async Task<PetsDto?> GetByIdPetAsync(int id, CancellationToken cancellation = default)
    {
        var pets = await _repository.GetByIdAsync(id, cancellation);
        if (pets == null) return null;

        return MapToDto(pets);
    }

    public async Task<int> CreatePetAsync(CreatePetsDto dto, CancellationToken cancellation = default)
    {
        var pets = new Pet(dto.Name, dto.Colour, dto.Age);
        await _repository.AddAsync(pets);

        return pets.Id;
    }

    public async Task UpdatePetAsync(int id, UpdatePetsDto dto, CancellationToken cancellation = default)
    {
        var pet = await _repository.GetByIdAsync(id, cancellation);

        if (pet == null) throw new ArgumentException($"No se encontro ninguna mascota con ID {id}");

        pet.PetUpdate(dto.Name, dto.Colour, dto.Age);
        await _repository.UpdateAsync(pet, cancellation);


    }
}
