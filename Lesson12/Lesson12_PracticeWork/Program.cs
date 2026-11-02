using System;
using System.Collections.Generic;

class Program
{
    // Використовуємо Dictionary<string, string>, де ключ - логін, а значення - пароль
    static Dictionary<string, string> EmployeeDatabase = new Dictionary<string, string>();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Менеджмент співробітників та паролів ---");
            Console.WriteLine("1. Додати співробітника (логін і пароль)");
            Console.WriteLine("2. Видалити співробітника за логіном");
            Console.WriteLine("3. Змінити логін або пароль співробітника");
            Console.WriteLine("4. Отримати пароль за логіном");
            Console.WriteLine("5. Показати всіх співробітників");
            Console.WriteLine("6. Вихід");
            Console.Write("Виберіть дію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee();
                    break;
                case "2":
                    DeleteEmployee();
                    break;
                case "3":
                    UpdateEmployee();
                    break;
                case "4":
                    GetPassword();
                    break;
                case "5":
                    ShowAllEmployees();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    // 1. Додати логін і пароль співробітника
    static void AddEmployee()
    {
        Console.Write("Введіть логін співробітника: ");
        string login = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(login))
        {
            Console.WriteLine("Логін не може бути порожнім!");
            return;
        }

        if (EmployeeDatabase.ContainsKey(login))
        {
            Console.WriteLine("Помилка: Співробітник з таким логіном вже існує.");
            return;
        }

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        EmployeeDatabase.Add(login, password);
        Console.WriteLine("Співробітника успішно додано!");
    }

    // 2. Видалити логін співробітника
    static void DeleteEmployee()
    {
        Console.Write("Введіть логін для видалення: ");
        string login = Console.ReadLine().Trim();

        if (EmployeeDatabase.Remove(login))
        {
            Console.WriteLine($"Співробітника з логіном '{login}' успішно видалено.");
        }
        else
        {
            Console.WriteLine("Помилка: Співробітника з таким логіном не знайдено.");
        }
    }

    // 3. Змінити інформацію про логін і пароль співробітника
    static void UpdateEmployee()
    {
        Console.Write("Введіть поточний логін співробітника, дані якого треба змінити: ");
        string oldLogin = Console.ReadLine().Trim();

        if (!EmployeeDatabase.ContainsKey(oldLogin))
        {
            Console.WriteLine("Помилка: Співробітника не знайдено.");
            return;
        }

        string currentPassword = EmployeeDatabase[oldLogin];

        Console.WriteLine("Що саме ви хочете змінити?");
        Console.WriteLine("1 - Тільки логін");
        Console.WriteLine("2 - Тільки пароль");
        Console.WriteLine("3 - І логін, і пароль");
        Console.Write("Вибір: ");
        string action = Console.ReadLine();

        if (action == "1" || action == "3")
        {
            Console.Write("Введіть новий логін: ");
            string newLogin = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(newLogin))
            {
                Console.WriteLine("Логін не може бути порожнім!");
                return;
            }

            if (EmployeeDatabase.ContainsKey(newLogin) && newLogin != oldLogin)
            {
                Console.WriteLine("Помилка: Цей логін уже зайнятий іншим співробітником.");
                return;
            }

            if (action == "3")
            {
                Console.Write("Введіть новий пароль: ");
                currentPassword = Console.ReadLine();
            }

            // Оскільки ключ у Dictionary не можна змінити напряму, видаляємо старий і додаємо новий
            EmployeeDatabase.Remove(oldLogin);
            EmployeeDatabase.Add(newLogin, currentPassword);
            Console.WriteLine("Дані успішно оновлено!");
        }
        else if (action == "2")
        {
            Console.Write("Введіть новий пароль: ");
            string newPassword = Console.ReadLine();
            EmployeeDatabase[oldLogin] = newPassword;
            Console.WriteLine("Пароль успішно змінено!");
        }
        else
        {
            Console.WriteLine("Некоректний вибір.");
        }
    }

    // 4. Отримати інформацію про пароль за логіном
    static void GetPassword()
    {
        Console.Write("Введіть логін співробітника: ");
        string login = Console.ReadLine().Trim();

        if (EmployeeDatabase.TryGetValue(login, out string password))
        {
            Console.WriteLine($"Пароль для логіна '{login}': {password}");
        }
        else
        {
            Console.WriteLine("Помилка: Співробітника з таким логіном не знайдено.");
        }
    }

    // Додатковий метод для перевірки списку
    static void ShowAllEmployees()
    {
        if (EmployeeDatabase.Count == 0)
        {
            Console.WriteLine("База даних порожня.");
            return;
        }

        Console.WriteLine("\nСписок співробітників:");
        foreach (var kvp in EmployeeDatabase)
        {
            Console.WriteLine($"Логін: {kvp.Key} | Пароль: {kvp.Value}");
        }
    }
}
