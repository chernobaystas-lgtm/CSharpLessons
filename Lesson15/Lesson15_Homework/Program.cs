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

        WriteLine("=== ТЕСТ 1: ПЕРЕВІРКА РОБОТИ БЛОКУ USING (IDisposable) ===");

        using (Play play1 = Play.Create("Гамлет", "Вільям Шекспір", "Трагедія", 1600))
        {
            WriteLine($"Створено об'єкт: {play1.ToString()}");
        }

        using (Shop shop1 = Shop.Create("Сільпо", "вул. Хрещатик, 1", "Продовольчий"))
        {
            WriteLine($"Створено об'єкт: {shop1.ToString()}");
        }


        WriteLine("\n=== ТЕСТ 2: ПЕРЕВІРКА РОБОТИ ДЕСТРУКТОРІВ ЧЕРЕЗ ЗБИРАЧ СМІТТЯ ===");

        // Створюємо об'єкти без using у локальному блоці, щоб втратити посилання
        {
            Play play2 = Play.Create("Лісова Пісня", "Леся Українка", "Драма", 1911);
            Shop shop2 = Shop.Create("Бутік", "вул. Центральна, 5", "Одяг");
            WriteLine("Тимчасові об'єкти створено, зараз вийдемо з блоку...");
        }

        WriteLine("Об'єкти залишилися без посилань. Примусово кличемо GC...");
        Collect();
        WaitForPendingFinalizers();

        WriteLine("\n=== Тестування Завдання 3 успішно завершено ===");
    }
}