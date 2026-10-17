using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson4_Homework
{
    // Клас «Співробітник»
    class Employee
    {
        // Поля класу закриті, доступ до них тільки через методи
        private string fullName = "";
        private DateTime birthDate;
        private string phone = "";
        private string email = "";
        private string position = "";
        private string jobDescription = "";

        // Введення даних з клавіатури
        public void Input()
        {
            Write("ПІБ: ");
            fullName = ReadLine()!;

            // повторюємо запит, поки не введуть коректну дату
            while (true)
            {
                Write("Дата народження (дд.мм.рррр): ");

                if (DateTime.TryParseExact(ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out birthDate))
                {
                    break;
                }

                WriteLine("Помилка: невірний формат дати! Приклад: 22.12.1990");
            }

            Write("Контактний телефон: ");
            phone = ReadLine()!;

            Write("Робочий email: ");
            email = ReadLine()!;

            Write("Посада: ");
            position = ReadLine()!;

            Write("Опис службових обов'язків: ");
            jobDescription = ReadLine()!;
        }

        // Виведення даних на екран
        public void Print()
        {
            WriteLine($"ПІБ: {fullName}");
            WriteLine($"Дата народження: {birthDate:dd.MM.yyyy}");
            WriteLine($"Контактний телефон: {phone}");
            WriteLine($"Робочий email: {email}");
            WriteLine($"Посада: {position}");
            WriteLine($"Службові обов'язки: {jobDescription}");
        }

        // Доступ до окремих полів: отримати значення
        public string GetFullName() { return fullName; }
        public DateTime GetBirthDate() { return birthDate; }
        public string GetPhone() { return phone; }
        public string GetEmail() { return email; }
        public string GetPosition() { return position; }
        public string GetJobDescription() { return jobDescription; }

        // Доступ до окремих полів: змінити значення
        public void SetFullName(string value) { fullName = value; }
        public void SetBirthDate(DateTime value) { birthDate = value; }
        public void SetPhone(string value) { phone = value; }
        public void SetEmail(string value) { email = value; }
        public void SetPosition(string value) { position = value; }
        public void SetJobDescription(string value) { jobDescription = value; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 6: клас «Співробітник»
            Employee employee = new Employee();

            WriteLine("Введіть дані співробітника:");
            employee.Input();

            WriteLine();
            WriteLine("Дані співробітника:");
            employee.Print();

            // доступ до окремих полів через методи класу
            WriteLine();
            WriteLine($"Посада (через GetPosition): {employee.GetPosition()}");

            Write("Введіть новий телефон: ");
            employee.SetPhone(ReadLine()!);

            WriteLine();
            WriteLine("Дані після зміни телефону:");
            employee.Print();
        }
    }
}