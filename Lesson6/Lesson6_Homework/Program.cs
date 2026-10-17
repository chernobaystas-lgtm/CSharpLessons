using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson6_PracticeWork;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        // Завдання 1: клас «Співробітник» із заробітною платою та перевантаженням операторів
        try
        {
            Employee vaska = new Employee();
            vaska.Input();

            int bonus = ReadInt("Введіть премію: ");
            vaska = vaska + bonus;

            int cut = ReadInt("Введіть, на скільки зменшити зарплату: ");
            vaska = vaska - cut;

            WriteLine();
            WriteLine("Дані співробітника:");
            vaska.Show();

            Employee boss = new Employee("Іван Іванович", new DateTime(1985, 5, 12),
                "+380991234567", "boss@company.com", "Директор", "Управління", 80000);

            WriteLine();
            WriteLine($"Зарплата директора: {boss.Salary}");

            if (vaska > boss)
            {
                WriteLine("Васька заробляє більше за директора");
            }
            else if (vaska < boss)
            {
                WriteLine("Васька заробляє менше за директора");
            }

            if (vaska == boss)
            {
                WriteLine("Зарплати однакові");
            }

            if (vaska != boss)
            {
                WriteLine("Зарплати різні");
            }

            if (vaska.Equals(boss))
            {
                WriteLine("Equals підтверджує рівність зарплат");
            }
            else
            {
                WriteLine("Equals: зарплати не рівні");
            }
        }
        catch (ArgumentException ex)
        {
            WriteLine($"Помилка: {ex.Message}");
        }
    }

    // Запитує ціле число не менше нуля, поки не введуть коректне
    static int ReadInt(string message)
    {
        while (true)
        {
            Write(message);

            if (int.TryParse(ReadLine(), out int value) && value >= 0)
            {
                return value;
            }

            WriteLine("Помилка: потрібно ввести ціле число не менше нуля.");
        }
    }
}

public class Employee
{
    private string fullName = "";
    private DateTime birthDate;
    private string phone = "";
    private string email = "";
    private string position = "";
    private string duties = "";
    private int salary;

    public Employee()
    {
    }

    public Employee(string fullName, DateTime birthDate, string phone,
                    string email, string position, string duties, int salary)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Phone = phone;
        Email = email;
        Position = position;
        Duties = duties;
        Salary = salary;
    }

    public string FullName
    {
        get { return fullName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ПІБ не може бути порожнім");
            fullName = value.Trim();
        }
    }

    public DateTime BirthDate
    {
        get { return birthDate; }
        set
        {
            if (value > DateTime.Today || value.Year < 1900)
                throw new ArgumentException("Некоректна дата народження");
            birthDate = value;
        }
    }

    public string Phone
    {
        get { return phone; }
        set
        {
            string cleaned = (value ?? "").Replace(" ", "").Replace("-", "")
                                          .Replace("(", "").Replace(")", "");
            string digits = cleaned.StartsWith("+") ? cleaned.Substring(1) : cleaned;

            if (digits.Length < 10 || digits.Length > 15 || !IsDigits(digits))
                throw new ArgumentException("Телефон має містити від 10 до 15 цифр");

            phone = cleaned;
        }
    }

    public string Email
    {
        get { return email; }
        set
        {
            value = (value ?? "").Trim();
            int at = value.IndexOf('@');

            if (at <= 0 || at != value.LastIndexOf('@') || value.Contains(' ') ||
                value.IndexOf('.', at) <= at + 1 || value.EndsWith("."))
                throw new ArgumentException("Невірний формат email, приклад: name@company.com");

            email = value;
        }
    }

    public string Position
    {
        get { return position; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Посада не може бути порожньою");
            position = value.Trim();
        }
    }

    public string Duties
    {
        get { return duties; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Опис обов'язків не може бути порожнім");
            duties = value.Trim();
        }
    }

    // Заробітна плата. Усі оператори та Show працюють саме з цим полем
    public int Salary
    {
        get { return salary; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Зарплата не може бути від'ємною");
            salary = value;
        }
    }

    public void Input()
    {
        FullName = Ask("ПІБ: ");

        while (true)
        {
            string dateInput = Ask("Дата народження (дд.мм.рррр): ");
            if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out DateTime date))
            {
                BirthDate = date;
                break;
            }
            WriteLine("Невірний формат дати, приклад: 25.03.1990. Спробуйте ще раз.");
        }

        Phone = Ask("Телефон: ");
        Email = Ask("Робочий email: ");
        Position = Ask("Посада: ");
        Duties = Ask("Опис службових обов'язків: ");

        while (true)
        {
            string salaryInput = Ask("Зарплата: ");
            if (int.TryParse(salaryInput, out int value) && value >= 0)
            {
                Salary = value;
                break;
            }
            WriteLine("Потрібно ввести ціле число не менше нуля. Спробуйте ще раз.");
        }
    }

    private string Ask(string message)
    {
        Write(message);
        return ReadLine() ?? "";
    }

    public void Show()
    {
        WriteLine($"ПІБ: {fullName}");
        WriteLine($"Дата народження: {birthDate:dd.MM.yyyy}");
        WriteLine($"Телефон: {phone}");
        WriteLine($"Email: {email}");
        WriteLine($"Посада: {position}");
        WriteLine($"Обов'язки: {duties}");
        WriteLine($"Зарплата: {salary}");
    }

    private static bool IsDigits(string s)
    {
        foreach (char c in s)
        {
            if (c < '0' || c > '9') return false;
        }
        return true;
    }

    public override string ToString()
    {
        return $"ПІБ: {fullName}\n" +
               $"Дата народження: {birthDate:dd.MM.yyyy}\n" +
               $"Телефон: {phone}\n" +
               $"Email: {email}\n" +
               $"Посада: {position}\n" +
               $"Обов'язки: {duties}\n" +
               $"Зарплата: {salary}";
    }

    // Збільшення зарплати на вказану кількість. Повертає нового співробітника
    public static Employee operator +(Employee employee, int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сума не може бути від'ємною");

        return employee.WithSalary(employee.salary + amount);
    }

    // Зменшення зарплати на вказану кількість. Повертає нового співробітника
    public static Employee operator -(Employee employee, int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сума не може бути від'ємною");

        if (amount > employee.salary)
            throw new ArgumentException("Зарплата не може стати від'ємною");

        return employee.WithSalary(employee.salary - amount);
    }

    // Перевірка на рівність зарплат
    public static bool operator ==(Employee? employee1, Employee? employee2)
    {
        if (ReferenceEquals(employee1, employee2)) return true;
        if (employee1 is null || employee2 is null) return false;

        return employee1.salary == employee2.salary;
    }

    public static bool operator !=(Employee? employee1, Employee? employee2)
    {
        return !(employee1 == employee2);
    }

    // Перевірка на більшу зарплату
    public static bool operator >(Employee employee1, Employee employee2)
    {
        return employee1.salary > employee2.salary;
    }

    // Перевірка на меншу зарплату
    public static bool operator <(Employee employee1, Employee employee2)
    {
        return employee1.salary < employee2.salary;
    }

    // Equals узгоджений з ==: порівнює зарплати
    public override bool Equals(object? obj)
    {
        return obj is Employee other && salary == other.salary;
    }

    public override int GetHashCode()
    {
        return salary.GetHashCode();
    }

    // Копія співробітника з іншою зарплатою (оригінал не змінюється)
    private Employee WithSalary(int newSalary)
    {
        return new Employee(fullName, birthDate, phone, email, position, duties, newSalary);
    }
}