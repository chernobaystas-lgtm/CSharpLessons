namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;

class Shop : IDisposable
{
    private string? _name { get; set; }
    private string? _address { get; set; }
    private string? _type { get; set; }

    private bool _disposed = false;

    // Конструктор за замовчуванням
    public Shop()
    {
        _name = "Без назви";
        _address = "Без адреси";
        _type = "Продовольчий";
    }

    // Конструктор з параметрами
    public Shop(string? name, string? address, string? type)
    {
        _name = name;
        _address = address;
        _type = type;
    }

    public override string ToString()
    {
        return $"Магазин: \"{_name}\", Адреса: {_address}, Тип: {_type}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Shop other)
        {
            return _name == other._name && _address == other._address && _type == other._type;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_name, _address, _type);
    }

    // Статичний метод для валідації та безпечного створення об'єкта
    public static Shop Create(string name, string address, string type)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва магазину не може бути порожньою.");
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Адреса магазину не може бути порожньою.");

        string[] validTypes = { "Продовольчий", "Господарський", "Одяг", "Взуття" };
        if (!Array.Exists(validTypes, t => t.Equals(type, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("Некоректний тип магазину (Дозволено: Продовольчий, Господарський, Одяг, Взуття).");
        }

        return new Shop(name, address, type);
    }

    // Метод для введення даних з консолі з захистом від сміття
    public void input()
    {
        while (true)
        {
            try
            {
                Write("Введіть назву магазину: ");
                string name = ReadLine() ?? "";
                Write("Введіть адресу магазину: ");
                string address = ReadLine() ?? "";
                Write("Введіть тип магазину (Продовольчий, Господарський, Одяг, Взуття): ");
                string type = ReadLine() ?? "";

                Shop valid = Shop.Create(name, address, type);
                _name = valid._name;
                _address = valid._address;
                _type = valid._type;
                break;
            }
            catch (Exception ex)
            {
                WriteLine($"\n[Помилка]: {ex.Message} Будь ласка, спробуйте знову.\n");
            }
        }
    }

    // Реалізація інтерфейсу IDisposable за умовою Завдання 2
    public void Dispose()
    {
        Dispose(true);
        // SuppressFinalize не потрібен, бо деструктора в цьому завданні ще немає
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            WriteLine($"[Dispose]: Керовані ресурси для магазину \"{_name}\" успішно звільнено вручну.");
            _name = null;
            _address = null;
            _type = null;
        }

        _disposed = true;
    }
}

