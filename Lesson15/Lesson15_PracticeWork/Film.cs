namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;



class Film : IDisposable
{
  private string? _title{get;set;}
  private int? _year{get;set;}
  private string? _director{get;set;}
  private DateTime? _time{get;set;} //Timespan
  private string? _genre{get;set;}

  public Film()
  {
    _title = "WIthout title";
    _year = 1950;
    _director = "Unknown";
    _time = DateTime.MinValue;
    _genre = "Unknown";
  }

  public Film(string? _title, int? _year, string? _director, DateTime? _time, string? _genre)
  {
    this._title = _title;
    this._year = _year;
    this._director = _director;
    this._time = _time;
    this._genre = _genre;
  }

  public override string ToString()
  {
    return $"Title: {_title}, Year: {_year}, Director: {_director}, Time: {_time}, Genre: {_genre}";
  }

  public override bool Equals(object? obj)
  {
    if (obj is Film film)
    {
      return _title == film._title && _year == film._year && _director == film._director && _time == film._time && _genre == film._genre;
    }
    return false;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(_title, _year, _director, _time, _genre);
  }

public static Film Create(string title, int year, string director, DateTime time, string genre)
{
    if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Название фильма не может быть пустым.");

    if (string.IsNullOrWhiteSpace(director))
        throw new ArgumentException("Режиссер не может быть пустым.");

    if (string.IsNullOrWhiteSpace(genre))
        throw new ArgumentException("Жанр не может быть пустым.");

    if (year < 1888 || year > DateTime.Now.Year)
        throw new ArgumentOutOfRangeException(nameof(year), $"Год должен быть в диапазоне от 1888 до {DateTime.Now.Year}.");

    return new Film
    {
        _title = title,
        _year = year,
        _director = director,
        _time = time,
        _genre = genre
    };
}


public void input()
{
    while (true)
    {
        try
        {
            Write("Enter the title of the film: ");
            string title = ReadLine() ?? "";

            Write("Enter the year of the film: ");
            int year = int.Parse(ReadLine() ?? "0");

            Write("Enter the director of the film: ");
            string director = ReadLine() ?? "";

            Write("Enter the duration of the film (e.g., 02:15): ");
            DateTime time = DateTime.Parse(ReadLine() ?? "");

            Write("Enter the genre of the film: ");
            string genre = ReadLine() ?? "";

            Film validFilm = Film.Create(title, year, director, time, genre);

            _title = validFilm._title;
            _year = validFilm._year;
            _director = validFilm._director;
            _time = validFilm._time;
            _genre = validFilm._genre;

            break;
        }
        catch (Exception ex)
        {
            WriteLine($"\n[Error]: {ex.Message} Please try entering the film details again.\n");
        }
    }
}


private bool _disposed = false;

~Film()
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
        WriteLine($"[Dispose]: Керовані ресурси для фільму \"{_title}\" успішно звільнено та вилучено з пам'яті.");

        _title = null;
        _director = null;
        _genre = null;
    }
    else
    {
        WriteLine("[Фіналізатор]: Об'єкт знищено автоматично збирачем сміття (GC).");
    }

    _disposed = true;
}



}

