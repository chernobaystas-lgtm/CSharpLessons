namespace LessonsMainCSharp;
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

        WriteLine("=== ТЕСТ ЗАВДАННЯ 2: ВИКЛИК МЕТОДУ DISPOSE ЧЕРЕЗ USING ===");

        // Тестування коректної роботи класу за допомогою блоку using
        using (Shop myShop = Shop.Create("Сільпо", "вул. Хрещатик, 1", "Продовольчий"))
        {
            WriteLine($"Об'єкт успішно створено: {myShop.ToString()}");
            WriteLine("Зараз блок using завершиться...");
        } // <- Тут автоматично викликається Dispose()

        WriteLine("\n=== Тестування Завдання 2 завершено ===");
    }
}