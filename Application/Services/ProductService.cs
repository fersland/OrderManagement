using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public record ProductDto(Guid Id, string Description, decimal Price, int Stock);
public record ProductCreateDto(string Description, decimal Price, int Stock);

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    private static ProductDto MapToDto(Product product) => new(
        product.Id,
        product.Description,
        product.Price,
        product.Stock
    );

    public async Task<IEnumerable<ProductDto>> GetAllProductAsync(CancellationToken cancellation = default)
    {
        var products = await _repository.GetAllAsync(cancellation);
        return products.Select(MapToDto);
    }

    public async Task<ProductDto?> GetProductById(Guid id, CancellationToken cancellation = default) {
        var product = await _repository.GetByIdAsync(id, cancellation);

        if (product == null) return null;
        return MapToDto(product);
    }

    public async Task<Guid> CreateProductAsync(ProductCreateDto dto, CancellationToken cancellation = default)
    {
        var product = new Product(dto.Description, dto.Price, dto.Stock);

        await _repository.AddAsync(product);
        return product.Id;
    }

}
