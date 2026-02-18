namespace HexagonalDemo.Domain.Entities;

/// <summary>
/// Сутність "Товар" (Product).
/// Це частина Доменного шару (Core), яка не залежить від бази даних чи API.
/// Вона представляє чисту бізнес-модель.
/// </summary>
public class Product
{
    /// <summary>
    /// Унікальний ідентифікатор товару.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва товару.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Ціна товару.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Фото товару (посилання або шлях).
    /// </summary>
    public string Photo { get; set; } = string.Empty;

    // Тут може бути бізнес-логіка, наприклад, валідація ціни або зміна назви.
    // Але жодної логіки збереження в БД!
}
