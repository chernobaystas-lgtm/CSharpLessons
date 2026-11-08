using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        WriteLine("=== ТЕСТ ЗАВДАННЯ 1: СТВОРЕННЯ ОБ'ЄКТА ТА РОБОТА ДЕСТРУКТОРА ===");

        // Локальний блок: створюємо об'єкт всередині дужок, щоб він втратив посилання
        {
            Play temporaryPlay = Play.Create("Лісова Пісня", "Леся Українка", "Драма", 1911);
            WriteLine($"Тимчасовий об'єкт успішно створено: {temporaryPlay.ToString()}");
            WriteLine("Виходимо з блоку, втрачаючи посилання на об'єкт...");
        }

        WriteLine("\nОб'єкт залишився без посилань у пам'яті.");
        WriteLine("Примусово кличемо Сборщик мусора (GC) для активації деструктора...");

        // Будимо збирач сміття (завдяки твоїм using static викликаємо напряму)
        Collect();
        WaitForPendingFinalizers();

        WriteLine("\n=== Тестування Завдання 1 завершено ===");
    }
}