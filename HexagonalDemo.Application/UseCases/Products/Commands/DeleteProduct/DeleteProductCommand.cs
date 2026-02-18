using MediatR;
using HexagonalDemo.Application.Ports;

namespace HexagonalDemo.Application.UseCases.Products.Commands.DeleteProduct;

/// <summary>
/// Команда: Видалити продукт за ID.
/// </summary>
public record DeleteProductCommand(int Id) : IRequest;

/// <summary>
/// Обробник команди видалення продукту.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
    }
}
