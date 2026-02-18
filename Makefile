# Змінні
PROJECT_NAME = HexagonalDemo
SLN_FILE = $(PROJECT_NAME).slnx
STARTUP_PROJECT = $(PROJECT_NAME).Presentation

.PHONY: all build run clean help restore

# За замовчуванням виводимо допомогу
all: help

## 🛠️ Основні команди

# Збірка проєкту
build:
	@echo "🔨 Компіляція проєкту..."
	dotnet build $(SLN_FILE)

# Запуск проєкту (API)
run:
	@echo "🚀 Запуск $(STARTUP_PROJECT)..."
	dotnet run --project $(STARTUP_PROJECT)/$(STARTUP_PROJECT).csproj

# Очищення (видалення bin/obj папок)
clean:
	@echo "🧹 Очищення..."
	dotnet clean $(SLN_FILE)

# Відновлення пакетів NuGet
restore:
	@echo "📦 Відновлення пакетів..."
	dotnet restore $(SLN_FILE)

# Запуск тестів (якщо будуть)
test:
	@echo "🧪 Запуск тестів..."
	dotnet test $(SLN_FILE)

## ℹ️ Допомога
help:
	@echo "Доступні команди:"
	@echo "  make build   - скомпілювати рішення"
	@echo "  make run     - запустити Web API"
	@echo "  make clean   - очистити тимчасові файли"
	@echo "  make restore - відновити залежності"
	@echo "  make test    - запустити тести"
