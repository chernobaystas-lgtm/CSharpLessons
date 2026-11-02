using System;
using System.Collections.Generic;

class Program
{
    // Організуємо звичайну чергу відвідувачів
    static Queue<string> RegularQueue = new Queue<string>();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Симуляція черги в кафе ---");
            Console.WriteLine("1. Новий відвідувач стає в чергу");
            Console.WriteLine("2. Звільнився столик (обслуговування)");
            Console.WriteLine("3. Прийшов відвідувач із резервом часу (позачергово)");
            Console.WriteLine("4. Показати поточну чергу");
            Console.WriteLine("5. Вихід");
            Console.Write("Виберіть дію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    EnterQueue();
                    break;
                case "2":
                    FreeTable();
                    break;
                case "3":
                    EnterWithReservation();
                    break;
                case "4":
                    ShowQueue();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    // 1. Відвідувачі приходять та потрапляють у чергу, якщо немає вільних місць
    static void EnterQueue()
    {
        Console.Write("Введіть ім'я відвідувача: ");
        string name = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Ім'я не може бути порожнім!");
            return;
        }

        RegularQueue.Enqueue(name);
        Console.WriteLine($"Відвідувач {name} став у чергу.");
    }

    // 2. Коли столик стає вільним, перший відвідувач з черги займає його
    static void FreeTable()
    {
        Console.WriteLine("Столик звільнився!");

        if (RegularQueue.Count > 0)
        {
            // Видаляємо першого з черги (FIFO)
            string visitor = RegularQueue.Dequeue();
            Console.WriteLine($"[Звичайна черга] Відвідувач {visitor} зайняв вільний столик.");
        }
        else
        {
            Console.WriteLine("Черга порожня. Вільний столик чекає на нових гостей.");
        }
    }

    // 3. Якщо приходить відвідувач, який резервував столик на певний час, він отримує його позачергово
    static void EnterWithReservation()
    {
        Console.Write("Введіть ім'я відвідувача з резервом: ");
        string name = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(name)) return;

        Console.Write("Введіть час резерву (наприклад, 18:30): ");
        string time = Console.ReadLine().Trim();

        // Позачергове обслуговування: відвідувач відразу отримує столик, минаючи чергу RegularQueue
        Console.WriteLine($"[ПОЗАЧЕРГОВО] Відвідувач {name} прийшов на свій час ({time}) і успішно зайняв зарезервований столик!");
    }

    // 4. Показати поточний стан черги
    static void ShowQueue()
    {
        if (RegularQueue.Count == 0)
        {
            Console.WriteLine("У черзі зараз нікого немає.");
            return;
        }

        Console.WriteLine($"\nЛюдей у черзі: {RegularQueue.Count}");
        Console.WriteLine("Порядок черги (зліва направо — від першого до останнього):");

        // Виводимо елементи черги без її зміни
        int position = 1;
        foreach (string visitor in RegularQueue)
        {
            Console.WriteLine($"{position}. {visitor}");
            position++;
        }
    }
}
