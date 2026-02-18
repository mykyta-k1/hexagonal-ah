using MediatR;
using HexagonalDemo.Application.Ports;
using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.UseCases.Products.Commands.CreateProduct;

/// <summary>
/// Команда: Створити новий продукт.
/// Повертає ID створеного продукту.
/// </summary>
public record CreateProductCommand(string Name, decimal Price, string Photo) : IRequest<int>;

/// <summary>
/// Обробник команди створення продукту.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Photo = request.Photo
        };

        // Тут може бути валідація
        if (product.Price < 0)
            throw new ArgumentException("Price cannot be negative");

        await _repository.AddAsync(product);

        return product.Id;
    }
}
