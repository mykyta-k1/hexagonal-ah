using MediatR;
using HexagonalDemo.Application.Ports;
using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.UseCases.Products.Queries.GetAllProducts;

/// <summary>
/// Запит: Отримати всі продукти.
/// </summary>
public record GetAllProductsQuery : IRequest<IEnumerable<Product>>;

/// <summary>
/// Обробник запиту на отримання всіх продуктів.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
