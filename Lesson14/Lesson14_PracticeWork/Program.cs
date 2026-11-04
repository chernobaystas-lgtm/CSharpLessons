namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       OutputEncoding = Encoding.UTF8;
       InputEncoding = Encoding.UTF8;


Laptop[] laptops = new[]
        {
            new Laptop("MacBook Pro 16", "Apple", 3.2, 10, 2500m, 2023),
            new Laptop("MacBook Air 13", "Apple", 2.8, 8, 1200m, 2022),
            new Laptop("ThinkPad X1", "Lenovo", 4.7, 14, 2100m, 2024),
            new Laptop("IdeaPad 3", "Lenovo", 2.4, 4, 550m, 2021),
            new Laptop("ROG Strix", "Asus", 5.0, 16, 2800m, 2024),
            new Laptop("ZenBook 14", "Asus", 3.0, 8, 1300m, 2023),
            new Laptop("Aspire 5", "Acer", 2.1, 4, 450m, 2020),
            new Laptop("Predator Helios", "Acer", 4.5, 12, 1900m, 2023),
            new Laptop("XPS 15", "Dell", 4.2, 14, 2300m, 2022),
            new Laptop("Vostro 3510", "Dell", 1.7, 2, 400m, 2019)
        };

        PrintSection("Вхідні дані (всі ноутбуки):");
        foreach (var laptop in laptops) Console.WriteLine(laptop); // record сам красиво отформатирует вывод!

        // Выполнение условий задачи с использованием LINQ
        PrintSection("Базова статистика кількості:");

        // • Підрахуйте кількість ноутбуків
        Console.WriteLine($"Загальна кількість ноутбуків: {laptops.Length}");

        // • Підрахуйте кількість ноутбуків з вартістю більше введеної (наприклад, 1500)
        decimal targetPrice = 1500m;
        Console.WriteLine($"Кількість дорожчих за {targetPrice}: {laptops.Count(l => l.Price > targetPrice)}");

        // • Підрахуйте кількість ноутбуків з вартістю в заданому діапазоні (наприклад, 500 - 1500)
        decimal minRange = 500m, maxRange = 1500m;
        Console.WriteLine($"Кількість у діапазоні {minRange} - {maxRange}: {laptops.Count(l => l.Price >= minRange && l.Price <= maxRange)}");

        // • Підрахуйте кількість ноутбуків певного виробника (наприклад, Asus)
        string targetBrand = "Asus";
        Console.WriteLine($"Кількість ноутбуків бренду {targetBrand}: {laptops.Count(l => l.Manufacturer == targetBrand)}");


        PrintSection("Пошук экстремумів:");

        // • Знайдіть ноутбук з мінімальною вартістю
        var cheapest = laptops.MinBy(l => l.Price);
        WriteLine($"Найдешевший ноутбук: {cheapest}");

        // • Знайдіть ноутбук з максимальною вартістю
        var mostExpensive = laptops.MaxBy(l => l.Price);
        WriteLine($"Найдорожчий ноутбук: {mostExpensive}");

        // • Виведіть інформацію про ноутбук з найменшою частотою процесора
        var lowestCpu = laptops.MinBy(l => l.CpuFrequency);
        WriteLine($"Ноутбук з найслабшим процесором: {lowestCpu}");

        // • Виведіть інформацію про найновішу модель ноутбуку
        var newest = laptops.MaxBy(l => l.Year);
        WriteLine($"Найновіша модель: {newest}");

        // • Знайдіть середню вартість ноутбуку
        WriteLine($"Середня вартість ноутбука: {laptops.Average(l => l.Price):F2}");


        PrintSection("Групування та статистика:");

        // • Виведіть статистику за кількістю ноутбуків кожного виробника
        var brandStats = laptops.GroupBy(l => l.Manufacturer);
        foreach (var group in brandStats)
        {
            WriteLine($"Виробник {group.Key}: {group.Count()} шт.");
        }

        // • Виведіть статистику за кількістю моделей ноутбуків (у нашому випадку кожна модель унікальна)
        WriteLine($"Всього унікальних моделей: {laptops.Select(l => l.Model).Distinct().Count()}");

        // • Виведіть статистику ноутбуків за роками
        var yearStats = laptops.GroupBy(l => l.Year).OrderBy(g => g.Key);
        foreach (var group in yearStats)
        {
            WriteLine($"Рік {group.Key}: {group.Count()} шт.");
        }


        PrintSection("Вибірки з використанням Skip та Take:");

        // • Виведіть п’ять найдорожчих ноутбуків
        var top5Expensive = laptops.OrderByDescending(l => l.Price).Take(5);
        PrintCollection("П'ять найдорожчих:", top5Expensive);

        // • Виведіть п’ять найдешевших ноутбуків
        var top5Cheapest = laptops.OrderBy(l => l.Price).Take(5);
        PrintCollection("П'ять найдешевших:", top5Cheapest);

        // • Виведіть три найстаріші моделі ноутбуків
        var top3Oldest = laptops.OrderBy(l => l.Year).Take(3);
        PrintCollection("Три найстаріші моделі:", top3Oldest);

        // • Виведіть три найновіші моделі ноутбуків
        var top3Newest = laptops.OrderByDescending(l => l.Year).Take(3);
        PrintCollection("Три найновіші моделі:", top3Newest);
    }

    // Хелпер-методы для красивого форматирования вывода в консоль
    private static void PrintSection(string title)
    {
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine($"\n=== {title.ToUpper()} ===");
        ResetColor();
    }

    private static void PrintCollection(string header, IEnumerable<Laptop> collection)
    {
        WriteLine($"--- {header} ---");
        foreach (var item in collection)
        {
            WriteLine($" > {item.Manufacturer} {item.Model} ({item.Year}р.) - {item.Price}$");
        }
    }
}

public record Laptop(
    string Model,
    string Manufacturer,
    double CpuFrequency,
    int CoresCount,
    decimal Price,
    int Year
);