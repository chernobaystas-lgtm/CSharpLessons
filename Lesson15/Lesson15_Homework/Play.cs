using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;

class Play
{
    private string? _title { get; set; }
    private string? _author { get; set; }
    private string? _genre { get; set; }
    private int? _year { get; set; }

    // Конструктор за замовчуванням
    public Play()
    {
        _title = "Без назви";
        _author = "Невідомий автор";
        _genre = "Невідомий жанр";
        _year = 1900;
    }

    // Конструктор з параметрами
    public Play(string? title, string? author, string? genre, int? year)
    {
        _title = title;
        _author = author;
        _genre = genre;
        _year = year;
    }

    public override string ToString()
    {
        return $"П'єса: \"{_title}\", Автор: {_author}, Жанр: {_genre}, Рік випуску: {_year}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Play other)
        {
            return _title == other._title && _author == other._author && _genre == other._genre && _year == other._year;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_title, _author, _genre, _year);
    }

    // Статичний метод для валідації та безпечного створення об'єкта
    public static Play Create(string title, string author, string genre, int year)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Назва п'єси не може бути порожньою.");
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("ПІБ автора не може бути порожнім.");
        if (string.IsNullOrWhiteSpace(genre)) throw new ArgumentException("Жанр не може бути порожнім.");
        if (year < 0 || year > DateTime.Now.Year) throw new ArgumentOutOfRangeException(nameof(year), "Некоректний рік випуску.");

        return new Play(title, author, genre, year);
    }

    // Метод для введення даних з консолі з захистом від сміття
    public void input()
    {
        while (true)
        {
            try
            {
                Write("Введіть назву п'єси: ");
                string title = ReadLine() ?? "";
                Write("Введіть ПІБ автора: ");
                string author = ReadLine() ?? "";
                Write("Введіть жанр: ");
                string genre = ReadLine() ?? "";
                Write("Введіть рік випуску: ");
                int year = int.Parse(ReadLine() ?? "0");

                Play valid = Play.Create(title, author, genre, year);
                _title = valid._title;
                _author = valid._author;
                _genre = valid._genre;
                _year = valid._year;
                break;
            }
            catch (Exception ex)
            {
                WriteLine($"\n[Помилка]: {ex.Message} Будь ласка, спробуйте знову.\n");
            }
        }
    }

    // ДЕСТРУКТОР (Фіналізатор) за умовою Завдання 1
    ~Play()
    {
        WriteLine($"[Деструктор]: Об'єкт п'єси \"{_title}\" успішно знищено автоматично збирачем сміття (GC).");
    }
}