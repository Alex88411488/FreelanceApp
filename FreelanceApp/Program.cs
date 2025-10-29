using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FreelanceApp.Data;
using FreelanceApp.Models;

namespace FreelanceApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // === 1. Чтение строки подключения из appsettings.json ===
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<FreelanceContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var db = new FreelanceContext(options);
            Console.WriteLine("=== Подключение к базе успешно ===\n");

            // === 2.1 Все категории ===
            Console.WriteLine("1️ Все категории услуг:");
            var categories = db.Categories.ToList();
            foreach (var c in categories)
                Console.WriteLine($" - {c.CategoryName}");
            Console.WriteLine();

            // === 2.2 Фрилансеры по специализации и рейтингу ===
            Console.WriteLine("2️ Фрилансеры по специализации 'дизайн' с рейтингом >= 4:");
            var topDesigners = db.VFreelancersWithRatings
                .Where(f => f.Specialization.Contains("дизайн") && f.AverageRating >= 4)
                .ToList();
            foreach (var f in topDesigners)
                Console.WriteLine($"{f.Name} — {f.Specialization} — {f.AverageRating}");
            Console.WriteLine();

            // === 2.3 Средний бюджет по категориям ===
            Console.WriteLine("3️ Средний бюджет по категориям:");
            var avgBudget = db.Projects
                .GroupBy(p => p.Category.CategoryName)
                .Select(g => new { Category = g.Key, Avg = g.Average(p => p.Budget) })
                .ToList();
            foreach (var item in avgBudget)
                Console.WriteLine($"{item.Category}: {item.Avg:C}");
            Console.WriteLine();

            // === 2.4 Два поля из связанных таблиц ===
            Console.WriteLine("4️ Проекты и заказчики:");
            var projClients = db.Projects
                .Include(p => p.Client)
                .Select(p => new { p.Title, ClientName = p.Client.Name })
                .Take(10)
                .ToList();
            foreach (var pc in projClients)
                Console.WriteLine($"{pc.Title} — {pc.ClientName}");
            Console.WriteLine();

            // === 2.5 Фильтрация проектов по категории и бюджету ===
            Console.WriteLine("5️ Проекты категории 'Web-разработка' с бюджетом > 1000:");
            var filteredProjects = db.Projects
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName.Contains("Web") && p.Budget > 1000)
                .ToList();
            foreach (var p in filteredProjects)
                Console.WriteLine($"{p.Title} — {p.Budget:C} — {p.Status}");
            Console.WriteLine();

            // === 2.6 Добавление клиента ===
            Console.WriteLine("6️ Добавляем нового клиента...");
            var client = new Client
            {
                Name = "Иван Иванов",
                ContactInfo = "ivan@example.com"
            };
            db.Clients.Add(client);
            db.SaveChanges();
            Console.WriteLine($"Добавлен клиент с ID = {client.ClientId}\n");

            // === 2.7 Добавление проекта для клиента ===
            Console.WriteLine("7️ Добавляем проект для клиента...");
            var project = new Project
            {
                Title = "Разработка сайта портфолио",
                Budget = 2000,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(14)), // ✅ Исправлено
                Status = "Поиск",
                CategoryId = categories.First().CategoryId,
                ClientId = client.ClientId
            };
            db.Projects.Add(project);
            db.SaveChanges();
            Console.WriteLine($"Добавлен проект с ID = {project.ProjectId}\n");

            // === 2.8 Удаление клиентов без проектов ===
            Console.WriteLine("8️ Удаляем клиентов без проектов...");
            var clientsToDelete = db.Clients
                .Include(c => c.Projects)
                .Where(c => !c.Projects.Any())
                .ToList();
            db.Clients.RemoveRange(clientsToDelete);
            db.SaveChanges();
            Console.WriteLine($"Удалено клиентов: {clientsToDelete.Count}\n");

            // === 2.9 Удаление откликов проекта ===
            Console.WriteLine("9️ Удаляем все отклики проекта с ID = " + project.ProjectId);
            var bidsToDelete = db.Bids.Where(b => b.ProjectId == project.ProjectId).ToList();
            db.Bids.RemoveRange(bidsToDelete);
            db.SaveChanges();
            Console.WriteLine($"Удалено откликов: {bidsToDelete.Count}\n");

            // === 2.10 Обновление рейтингов фрилансеров ===
            Console.WriteLine("10 Обновляем рейтинг фрилансеров с рейтингом < 3...");
            var lowRated = db.VFreelancersWithRatings
                .Where(f => f.AverageRating < 3)
                .Select(f => f.FreelancerId)
                .ToList();

            Console.WriteLine("Фрилансеры с рейтингом ниже 3:");
            foreach (var id in lowRated)
            {
                var fr = db.Freelancers.Find(id);
                if (fr != null)
                    Console.WriteLine($" - {fr.Name} (ID {fr.FreelancerId})");
            }

        }
    }
}
