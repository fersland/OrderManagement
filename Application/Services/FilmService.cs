using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services;

public record FilmsDto(int Id, string Title, string Description, int ReleaseYear);
public record CreateFilmsDto (string Title, string Description, int ReleaseYear);
public record UpdateFilmDto(string Title, string Description, int ReleaseYear);
public record UpdatedFilmDto(string Title, string Description, int ReleaseYear);
public record UpdateFilmYaDto(string Title, string Description, int ReleaseYear);

public class FilmService
{
    private readonly IFilmsRepository _repository;

    public FilmService(IFilmsRepository repository)
    {
        _repository = repository;
    }

    private static FilmsDto MapToDto(Films films) => new(
        films.Id,
        films.Title,
        films.Description,
        films.ReleaseYear
        );

    public async Task<IEnumerable<FilmsDto>> GetAllFilmsAsync(CancellationToken cancellation = default)
    {
        var filsm = await _repository.GetAllAsync(cancellation);
        return filsm.Select(MapToDto);
    }

    public async Task<FilmsDto?> GetByIdFilmsAsync(int id, CancellationToken cancellation = default)
    {
        var films = await _repository.GetByIdAsync(id, cancellation);
        if (films == null) return null;

        return MapToDto(films);
    }

    public async Task<int> CreateFilmsAsync(CreateFilmsDto dto, CancellationToken cancellation = default)
    {
        var films = new Films(dto.Title, dto.Description, dto.ReleaseYear);
        await _repository.AddAsync(films, cancellation);
        return films.Id;
    }

    public async Task UpdateFilmAsync(int id, UpdateFilmDto dto, CancellationToken cancellation = default)
    {
        var film = await _repository.GetByIdAsync(id, cancellation);
        if (film == null) throw new KeyNotFoundException($"No se encontro la pelicula con ID {id}");

        film.UpdateDetails(dto.Title, dto.Description, dto.ReleaseYear);
        await _repository.UpdateAsync(film, cancellation);
    }
}
