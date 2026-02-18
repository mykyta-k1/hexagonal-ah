using MediatR;
using Microsoft.EntityFrameworkCore;
// using HexagonalDemo.Infrastructure.Persistence; // Простір імен (placeholder)
// using HexagonalDemo.Application.UseCases; // Простір імен (placeholder)

var builder = WebApplication.CreateBuilder(args);

// Додавання сервісів до контейнера.

builder.Services.AddControllers();
// Дізнайтеся більше про налаштування Swagger/OpenAPI: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- НАЛАШТУВАННЯ ЗАЛЕЖНОСТЕЙ ГЕКСАГОНАЛЬНОЇ АРХІТЕКТУРИ ---

// 1. Інфраструктура (Adapters)
// Реєструємо InMemory репозиторій як Singleton, щоб дані зберігалися між запитами
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

// Реєструємо Proxy з HttpClient
builder.Services.AddHttpClient<IProductProxy, FakeStoreProductProxy>(client =>
{
    client.BaseAddress = new Uri("https://fakestoreapi.com/");
});

// 2. Application Services (Use Cases)
builder.Services.AddScoped<IProductService, ProductService>();

// 3. MediatR (якщо використовується)
// builder.Services.AddMediatR(...)

var app = builder.Build();

// Налаштування конвеєра HTTP запитів.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
