using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.Services;

/// <summary>
/// Інтерфейс сервісу продуктів (Вхідний Порт).
/// Визначає операції, які доступні для зовнішнього світу (UI, API).
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Отримати всі продукти.
    /// </summary>
    Task<IEnumerable<Product>> GetAllProductsAsync();

    /// <summary>
    /// Отримати продукт за ID.
    /// </summary>
    Task<Product?> GetProductByIdAsync(int id);

    /// <summary>
    /// Додати новий продукт.
    /// </summary>
    Task AddProductAsync(Product product);

    /// <summary>
    /// Видалити продукт за ID.
    /// </summary>
    Task DeleteProductAsync(int id);

    /// <summary>
    /// Знайти продукти за назвою.
    /// </summary>
    Task<IEnumerable<Product>> SearchProductsByNameAsync(string name);
}
