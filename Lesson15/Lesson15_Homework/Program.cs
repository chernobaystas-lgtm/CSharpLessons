using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;
using System.Diagnostics;


class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        WriteLine("=== ЗАВДАННЯ 4: КЕРУВАННЯ ПАМ'ЯТТЮ ТА ПОКОЛІННЯМИ ===");

        // Запуск підрахунку часу до збирання сміття
        Stopwatch timer = new Stopwatch();
        timer.Start();

        MemoryAllocator allocator = new MemoryAllocator();
        allocator.AllocateMemory();

        timer.Stop();
        WriteLine($"Час виділення пам'яті: {timer.ElapsedMilliseconds} мс");

        // Визначаємо покоління об'єкта одразу після створення (має бути 0)
        WriteLine($"Покоління масиву після створення: Покоління {allocator.GetArrayGeneration()}");

        WriteLine("\nВикликаємо збирач сміття (GC.Collect)...");
        // Викликаємо збирач сміття
        Collect();
        WaitForPendingFinalizers();

        // Перевіряємо, чи перемістився об'єкт у старше покоління (має стати 1)
        WriteLine($"Покоління масиву після збирання сміття: Покоління {allocator.GetArrayGeneration()}");

        WriteLine("\nЯвно звільняємо ресурси через Dispose...");

        // Заміряємо швидкість очищення ресурсів після збирання сміття
        timer.Restart();

        allocator.Dispose();

        timer.Stop();
        WriteLine($"Час очищення пам'яті: {timer.ElapsedMilliseconds} мс");

        WriteLine("\n=== Тестування Завдання 4 завершено ===");
    }
}