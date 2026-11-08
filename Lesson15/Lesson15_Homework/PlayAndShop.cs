using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;
class Play : IDisposable
{
    private string? _title { get; set; }
    private string? _author { get; set; }
    private string? _genre { get; set; }
    private int? _year { get; set; }

    private bool _disposed = false;

    public Play()
    {
        _title = "Без назви";
        _author = "Невідомий автор";
        _genre = "Невідомий жанр";
        _year = 1900;
    }

    public Play(string? title, string? author, string? genre, int? year)
    {
        _title = title;
        _author = author;
        _genre = genre;
        _year = year;
    }

    public override string ToString()
    {
        return $"П'єса: \"{_title}\", Автор: {_author}, Жанр: {_genre}, Рік: {_year}";
    }

    public static Play Create(string title, string author, string genre, int year)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Назва п'єси не може бути порожньою.");
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("ПІБ автора не може бути порожнім.");
        if (string.IsNullOrWhiteSpace(genre)) throw new ArgumentException("Жанр не може бути порожнім.");
        if (year < 0 || year > DateTime.Now.Year) throw new ArgumentOutOfRangeException(nameof(year), "Некоректний рік випуску.");

        return new Play(title, author, genre, year);
    }

    ~Play()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this); // Завдяки using static System.GC викликаємо напряму
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            WriteLine($"[Dispose П'єса]: Керовані ресурси для п'єси \"{_title}\" успішно звільнено вручну.");
            _title = null; _author = null; _genre = null;
        }
        else
        {
            WriteLine($"[Деструктор П'єса]: Об'єкт п'єси \"{_title}\" знищено автоматично збирачем сміття (GC).");
        }
        _disposed = true;
    }
}

class Shop : IDisposable
{
    private string? _name { get; set; }
    private string? _address { get; set; }
    private string? _type { get; set; }

    private bool _disposed = false;

    public Shop()
    {
        _name = "Без назви";
        _address = "Без адреси";
        _type = "Продовольчий";
    }

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

    public static Shop Create(string name, string address, string type)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Назва магазину не може бути порожньою.");
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Адреса магазину не може бути порожньою.");

        string[] validTypes = { "Продовольчий", "Господарський", "Одяг", "Взуття" };
        if (!Array.Exists(validTypes, t => t.Equals(type, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("Некоректний тип магазину.");
        }

        return new Shop(name, address, type);
    }

    ~Shop()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            WriteLine($"[Dispose Магазин]: Керовані資源 для магазину \"{_name}\" успішно звільнено вручну.");
            _name = null; _address = null; _type = null;
        }
        else
        {
            WriteLine($"[Деструктор Магазин]: Об'єкт магазину \"{_name}\" знищено автоматично збирачем сміття (GC).");
        }
        _disposed = true;
    }
}