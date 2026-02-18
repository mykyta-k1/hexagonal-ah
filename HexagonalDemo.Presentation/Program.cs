using MediatR;
using Microsoft.EntityFrameworkCore;
using HexagonalDemo.Infrastructure.Adapters;
using HexagonalDemo.Application.Ports;
using HexagonalDemo.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Додавання сервісів до контейнера.

builder.Services.AddControllers();
// Дізнайтеся більше про налаштування Swagger/OpenAPI: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
// Реєстрація MediatR. Ми вказуємо один з типів з проєкту Application (наприклад CreateProductCommand),
// щоб MediatR міг просканувати всю збірку і знайти всі Handlers.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<HexagonalDemo.Application.UseCases.Products.Commands.CreateProduct.CreateProductCommand>());

// Можна залишити і пряму реєстрацію сервісів, якщо потрібно
// builder.Services.AddScoped<IProductService, ProductService>();

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
