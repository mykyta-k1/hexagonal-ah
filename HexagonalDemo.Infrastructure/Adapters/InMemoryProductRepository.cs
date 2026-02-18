using System.Collections.Concurrent;
using HexagonalDemo.Application.Ports;
using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Infrastructure.Adapters;

/// <summary>
/// Адаптер для збереження продуктів у пам'яті.
/// Це спрощена реалізація "бази даних" для демонстрації.
/// </summary>
public class InMemoryProductRepository : IProductRepository
{
    // Використовуємо thread-safe колекцію, оскільки Singleton сервіс буде доступний з багатьох потоків
    private static readonly ConcurrentDictionary<int, Product> _products = new();
    
    // Лічильник для ID (імітуємо Auto Increment)
    private static int _nextId = 1;

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products.Values);
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        _products.TryGetValue(id, out var product);
        return Task.FromResult(product);
    }

    public Task AddAsync(Product product)
    {
        // Якщо ID не встановлено, генеруємо новий
        if (product.Id == 0)
        {
            product.Id = Interlocked.Increment(ref _nextId);
        }

        // AddOrUpdate гарантує, що ми не отримаємо помилку, хоча AddAsync зазвичай передбачає додавання нового.
        // Для спрощення просто записуємо.
        _products[product.Id] = product;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _products.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Product>> GetByNameAsync(string name)
    {
        var result = _products.Values
            .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        
        return Task.FromResult(result);
    }
}
