using System.Text.Json;
using System.Text.Json.Serialization;
using HexagonalDemo.Application.Ports;
using HexagonalDemo.Domain.Entities;

namespace HexagonalDemo.Infrastructure.Adapters;

/// <summary>
/// Адаптер для зовнішнього API (FakeStoreAPI).
/// Використовує HttpClient для отримання даних.
/// </summary>
public class FakeStoreProductProxy : IProductProxy
{
    private readonly HttpClient _httpClient;

    public FakeStoreProductProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Базовий URL можна винести в конфігурацію, але для спрощення залишимо тут або він буде переданий через DI
        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("https://fakestoreapi.com/");
        }
    }

    public async Task<Product?> GetExternalProductAsync(int id)
    {
        try
        {
            // Отримуємо дані з зовнішнього API
            var response = await _httpClient.GetAsync($"products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                // Якщо помилка (наприклад 404), повертаємо null
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var externalDto = JsonSerializer.Deserialize<FakeStoreProductDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (externalDto == null) return null;

            // Мапимо DTO на нашу Доменну Сутність
            return new Product
            {
                Id = externalDto.Id,
                Name = externalDto.Title, // Mapping Title -> Name
                Price = externalDto.Price,
                Photo = externalDto.Image // Mapping Image -> Photo
            };
        }
        catch (Exception)
        {
            // Логгування помилки мало б бути тут
            return null;
        }
    }

    // Внутрішній DTO клас для десеріалізації відповіді API
    // Це деталь реалізації Адаптера, вона не повинна "витікати" в Домен
    private class FakeStoreProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
