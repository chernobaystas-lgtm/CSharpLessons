using System;
using System.Text;
using static System.Console;

namespace Lesson3_Homework
{
    // Клас «Веб-сайт»
    class WebSite
    {
        // Поля класу закриті, доступ до них тільки через методи
        private string name = "";
        private string path = "";
        private string description = "";
        private string ipAddress = "";

        // Введення даних з клавіатури
        public void Input()
        {
            Write("Назва сайту: ");
            name = ReadLine()!;

            Write("Шлях до сайту (URL): ");
            path = ReadLine()!;

            Write("Опис сайту: ");
            description = ReadLine()!;

            // повторюємо запит, поки не введуть коректну ір-адресу
            while (true)
            {
                Write("ІР-адреса (наприклад, 192.168.0.1): ");
                string input = ReadLine()!;

                if (IsValidIp(input))
                {
                    ipAddress = input;
                    break;
                }

                WriteLine("Помилка: невірна ІР-адреса! Потрібно 4 числа від 0 до 255 через крапку.");
            }
        }

        // Виведення даних на екран
        public void Print()
        {
            WriteLine($"Назва сайту: {name}");
            WriteLine($"Шлях до сайту: {path}");
            WriteLine($"Опис сайту: {description}");
            WriteLine($"ІР-адреса: {ipAddress}");
        }

        // Доступ до окремих полів: отримати значення
        public string GetName() { return name; }
        public string GetPath() { return path; }
        public string GetDescription() { return description; }
        public string GetIpAddress() { return ipAddress; }

        // Доступ до окремих полів: змінити значення
        public void SetName(string value) { name = value; }
        public void SetPath(string value) { path = value; }
        public void SetDescription(string value) { description = value; }

        // Для ІР-адреси повертає true, якщо значення коректне і було збережене
        public bool SetIpAddress(string value)
        {
            if (!IsValidIp(value))
            {
                return false;
            }

            ipAddress = value;
            return true;
        }

        // Перевіряє, що рядок має вигляд ІР-адреси: 4 числа від 0 до 255 через крапку
        private static bool IsValidIp(string value)
        {
            string[] parts = value.Split('.');

            if (parts.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out int number) || number < 0 || number > 255)
                {
                    return false;
                }
            }

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 4: клас «Веб-сайт»
            WebSite site = new WebSite();

            WriteLine("Введіть дані веб-сайту:");
            site.Input();

            WriteLine();
            WriteLine("Дані веб-сайту:");
            site.Print();

            // доступ до окремих полів через методи класу
            WriteLine();
            WriteLine($"Назва (через GetName): {site.GetName()}");

            Write("Введіть нову ІР-адресу: ");
            if (site.SetIpAddress(ReadLine()!))
            {
                WriteLine();
                WriteLine("Дані після зміни ІР-адреси:");
                site.Print();
            }
            else
            {
                WriteLine("Помилка: невірна ІР-адреса, значення не змінено.");
            }
        }
    }
}