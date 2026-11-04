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

              Magazine[] magazines = new Magazine[]
        {
            new Magazine("Сад та город", "Сад та город", 45, 120.0, new DateTime(2022, 5, 12)),
            new Magazine("Політика сьогодні", "Політика", 35, 150.0, new DateTime(2023, 1, 10)),
            new Magazine("Спортивний світ", "Спорт", 60, 95.5, new DateTime(2022, 8, 24)),
            new Magazine("Мисливство та Рибалка", "Рибалка", 28, 110.0, new DateTime(2021, 11, 5)),
            new Magazine("Модні тренди", "Мода", 80, 250.0, new DateTime(2024, 3, 1)),
            new Magazine("Полювання в лісі", "Полювання", 50, 135.0, new DateTime(2022, 12, 15))
        };

        WriteLine("=== ВСІ ЖУРНАЛИ В МАСИВІ ===");
        foreach (var m in magazines) WriteLine(m);

        bool isMore30Pages = magazines.All(m => m.Pages > 30);
        WriteLine($"\nЧи всі журнали мають більше 30 сторінок? {isMore30Pages}");

        bool ISOnlyPoliticSportFashion = magazines.All(m => m.Category == "Політика" || m.Category == "Спорт" || m.Category == "Мода");
        WriteLine($"\nЧи всі журнали належать до категорій 'Політика', 'Спорт' або 'Мода'? {ISOnlyPoliticSportFashion}");

        bool IsAnyFarm = magazines.Any(m => m.Category == "Сад та город");
        WriteLine($"\nЧи є хоча б один журнал з категорії 'Сад та город'? {IsAnyFarm}");

        bool IsAnyfishing = magazines.Any(m => m.Category == "Рибалка");
        WriteLine($"\nЧи є хоча б один журнал з категорії 'Рибалка'? {IsAnyfishing}");

        bool hasHunting = magazines.Any(m => m.Title.Contains("Полювання", StringComparison.OrdinalIgnoreCase));
        WriteLine($"Чи є журнал про 'Полювання' (без урахування регістру): {hasHunting}");

          var first2022 = magazines.FirstOrDefault(m => m.ReleaseDate.Year == 2022);
        WriteLine($"\nПерший журнал за 2022 рік:\n{first2022}");

        var lastWithSvit = magazines.LastOrDefault(m => m.Title.StartsWith("Світ", StringComparison.OrdinalIgnoreCase));
        WriteLine($"\nОстанній журнал, що починається на 'Світ':\n{(lastWithSvit != null ? lastWithSvit.ToString() : "Не знайдено")}");

    }
}

record class Magazine(string Title, string Category, int Pages, double Price, DateTime ReleaseDate){}