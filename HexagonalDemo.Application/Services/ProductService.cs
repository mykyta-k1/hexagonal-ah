using HexagonalDemo.Application.Ports;
using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Application.Services;

/// <summary>
/// Реалізація сервісу продуктів (Use Case Interactor).
/// Цей клас керує бізнес-логікою та використовує порти (Repositories/Proxies).
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductProxy _productProxy;

    public ProductService(IProductRepository productRepository, IProductProxy productProxy)
    {
        _productRepository = productRepository;
        _productProxy = productProxy;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        // Логіка: просто повертаємо всі продукти з БД.
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        // Спочатку шукаємо в локальній БД
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            return product;
        }

        // Якщо немає в БД, пробуємо знайти в зовнішньому API (Proxy)
        var externalProduct = await _productProxy.GetExternalProductAsync(id);
        if (externalProduct != null)
        {
            // Можна зберегти в локальну БД, якщо це потрібно за бізнес-логікою (кешування)
            await _productRepository.AddAsync(externalProduct);
            return externalProduct;
        }

        return null;
    }

    public async Task AddProductAsync(Product product)
    {
        // Тут може бути валідація, наприклад, перевірка ціни
        if (product.Price < 0)
        {
            throw new ArgumentException("Ціна не може бути від'ємною");
        }

        await _productRepository.AddAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string name)
    {
        return await _productRepository.GetByNameAsync(name);
    }
}
