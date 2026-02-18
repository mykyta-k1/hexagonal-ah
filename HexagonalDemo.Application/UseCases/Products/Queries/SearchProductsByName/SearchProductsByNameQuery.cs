using MediatR;
using HexagonalDemo.Application.Ports;
using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.UseCases.Products.Queries.SearchProductsByName;

/// <summary>
/// Запит: Пошук продуктів за назвою.
/// </summary>
public record SearchProductsByNameQuery(string Name) : IRequest<IEnumerable<Product>>;

/// <summary>
/// Обробник запиту пошуку продуктів.
/// </summary>
public class SearchProductsByNameHandler : IRequestHandler<SearchProductsByNameQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repository;

    public SearchProductsByNameHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> Handle(SearchProductsByNameQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByNameAsync(request.Name);
    }
}
