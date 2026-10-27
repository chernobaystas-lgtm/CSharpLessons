using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson10_Homework;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        // Завдання 3: лямбда-вираз для перевірки, чи є день Днем програміста (256-й день року)
        Predicate<DateTime> isProgrammerDay = date => date.DayOfYear == 256;

        // Тест 1: кілька готових дат
        WriteLine("Тест на готових датах:");

        DateTime[] testDates =
        {
            new DateTime(2023, 9, 13),  // 256-й день у невисокосному році
            new DateTime(2024, 9, 12),  // 256-й день у високосному році
            new DateTime(2024, 9, 13),  // у високосному році це вже 257-й день
            new DateTime(2025, 1, 1),
            new DateTime(2025, 12, 31)
        };

        for (int i = 0; i < testDates.Length; i++)
        {
            string answer = isProgrammerDay(testDates[i]) ? "так" : "ні";

            WriteLine($"{testDates[i]:dd.MM.yyyy} (день року: {testDates[i].DayOfYear}) - День програміста: {answer}");
        }

        // Тест 2: дата від користувача
        WriteLine();
        Write("Введіть свою дату (дд.мм.рррр): ");

        if (!DateTime.TryParseExact(ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                    DateTimeStyles.None, out DateTime input))
        {
            WriteLine("Помилка: невірний формат дати! Приклад: 13.09.2023");
            return;
        }

        if (isProgrammerDay(input))
        {
            WriteLine("Так, це День програміста (256-й день року)!");
        }
        else
        {
            WriteLine($"Ні, це {input.DayOfYear}-й день року, а не 256-й.");
        }
    }
}