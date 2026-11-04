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
        foreach (var laptop in laptops) WriteLine(laptop);

        PrintSection("Базова статистика кількості:");

        WriteLine($"Загальна кількість ноутбуків: {laptops.Length}");

        decimal targetPrice = 1500m;
        WriteLine($"Кількість дорожчих за {targetPrice}: {laptops.Count(l => l.Price > targetPrice)}");

        decimal minRange = 500m, maxRange = 1500m;
        WriteLine($"Кількість у діапазоні {minRange} - {maxRange}: {laptops.Count(l => l.Price >= minRange && l.Price <= maxRange)}");

        string targetBrand = "Asus";
        WriteLine($"Кількість ноутбуків бренду {targetBrand}: {laptops.Count(l => l.Manufacturer == targetBrand)}");


        PrintSection("Пошук экстремумів:");

        var cheapest = laptops.MinBy(l => l.Price);
        WriteLine($"Найдешевший ноутбук: {cheapest}");

        var mostExpensive = laptops.MaxBy(l => l.Price);
        WriteLine($"Найдорожчий ноутбук: {mostExpensive}");

        var lowestCpu = laptops.MinBy(l => l.CpuFrequency);
        WriteLine($"Ноутбук з найслабшим процесором: {lowestCpu}");

        var newest = laptops.MaxBy(l => l.Year);
        WriteLine($"Найновіша модель: {newest}");

        WriteLine($"Середня вартість ноутбука: {laptops.Average(l => l.Price):F2}");


        PrintSection("Групування та статистика:");

        var brandStats = laptops.GroupBy(l => l.Manufacturer);
        foreach (var group in brandStats)
        {
            WriteLine($"Виробник {group.Key}: {group.Count()} шт.");
        }

        WriteLine($"Всього унікальних моделей: {laptops.Select(l => l.Model).Distinct().Count()}");

        var yearStats = laptops.GroupBy(l => l.Year).OrderBy(g => g.Key);
        foreach (var group in yearStats)
        {
            WriteLine($"Рік {group.Key}: {group.Count()} шт.");
        }


        PrintSection("Вибірки з використанням Skip та Take:");

        var top5Expensive = laptops.OrderByDescending(l => l.Price).Take(5);
        PrintCollection("П'ять найдорожчих:", top5Expensive);

        var top5Cheapest = laptops.OrderBy(l => l.Price).Take(5);
        PrintCollection("П'ять найдешевших:", top5Cheapest);

        var top3Oldest = laptops.OrderBy(l => l.Year).Take(3);
        PrintCollection("Три найстаріші моделі:", top3Oldest);

        var top3Newest = laptops.OrderByDescending(l => l.Year).Take(3);
        PrintCollection("Три найновіші моделі:", top3Newest);
    }

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