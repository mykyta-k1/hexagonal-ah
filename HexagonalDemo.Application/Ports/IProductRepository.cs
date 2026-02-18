using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.Ports;

/// <summary>
/// Порт (Інтерфейс) для роботи з репозиторієм продуктів.
/// Цей інтерфейс визначає, ЯКІ операції потрібні застосунку для роботи з даними.
/// Реалізація цього інтерфейсу (Адаптер) буде знаходитися в інфраструктурі (Database/SQL).
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Отримати всі продукти.
    /// </summary>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary>
    /// Отримати продукт за ID.
    /// </summary>
    Task<Product?> GetByIdAsync(int id);

    /// <summary>
    /// Додати новий продукт.
    /// </summary>
    Task AddAsync(Product product);
}
