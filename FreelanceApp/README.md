# FreelanceApp (вариант: фриланс-платформа)

Проект демонстрирует работу с Entity Framework Core + LINQ над предметной областью:
Категории услуг, Фрилансеры, Заказчики, Проекты, Отклики, Отзывы и рейтинги.

## Что реализовано
- Scaffolding (Database-First) или Code-First (модели в /Models).
- LINQ-запросы: выборки, группировка, join, фильтрация.
- CRUD операции (вставка/удаление/обновление).
- GitHub Actions workflow для сборки на двух платформах.
- README и badge статуса workflow.

  [![Build and Publish](https://github.com/Alex88411488/MyConsoleApp/actions/workflows/build.yml/badge.svg)](https://github.com/Alex88411488/MyConsoleApp/actions/workflows/build.yml)

## Быстрый старт
1. Клонируйте репозиторий.
2. Создайте файл `appsettings.json` (пример в проекте) и поместите корректную строку подключения:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;User Id=...;Password=...;"
  }
}
