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

// 1. Додавання MediatR (Шар застосунку - Application Layer)
// Сканує збірку Application для пошуку обробників (Handlers) та запитів (Requests).
// builder.Services.AddMediatR(typeof(HexagonalDemo.Application.AssemblyMarker)); 
// Примітка: Вам потрібно створити клас-маркер або використати будь-який тип з проєкту Application.
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

// 2. Додавання DbContext (Шар інфраструктури - Infrastructure Layer)
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Реєстрація Портів та Адаптерів (Ports & Adapters)
// builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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
