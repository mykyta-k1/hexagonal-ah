using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.Ports;

/// <summary>
/// Порт (Інтерфейс) для взаємодії із зовнішніми API.
/// Використовується для отримання додаткової інформації про продукти із зовнішніх джерел (наприклад, FakeStoreAPI).
/// Реалізація (Адаптер) буде використовувати HTTP клієнт.
/// </summary>
public interface IProductProxy
{
    /// <summary>
    /// Отримати дані про продукт із зовнішньої системи за ID.
    /// </summary>
    /// <param name="id">Ідентифікатор продукту в зовнішній системі.</param>
    /// <returns>Об'єкт Product або null, якщо не знайдено.</returns>
    Task<Product?> GetExternalProductAsync(int id);
}
