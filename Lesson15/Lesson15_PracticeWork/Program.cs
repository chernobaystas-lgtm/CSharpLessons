namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;


class Program
{
static void Main(string[] args)
{
    OutputEncoding = Encoding.UTF8;

    WriteLine("=== ЕТАП 1: РУЧНЕ УПРАВЛІННЯ (БЛОК USING) ===");
    using (Spectacle play1 = Spectacle.Create("Гамлет", "Національний театр", "Трагедія", 150, new List<string> { "Бенедикт Камбербетч", "Том Хіддлстон" }))
    {
        WriteLine($"Успішно створено виставу: {play1.ToString()}");
        WriteLine("Зараз блок using закриється...");
    }

    WriteLine("\n=== ЕТАП 2: АВТОМАТИЧНЕ ЗБИРАННЯ СМІТТЯ (GC) ===");
    CreateAndForgetSpectacle();

    WriteLine("Об'єкт втратив посилання і просто залишений у пам'яті.");
    WriteLine("Примусово кличемо Сборщик мусора...");

    Collect();
    WaitForPendingFinalizers();

    WriteLine("\n=== Збирання сміття завершено, програма завершує роботу ===");
}

static void CreateAndForgetSpectacle()
{
    Spectacle play2 = Spectacle.Create("Ромео і Джульєтта", "Театр Драми", "Мелодрама", 120, new List<string> { "Актор 1", "Актор 2" });
    WriteLine($"Створено другу виставу всередині методу: {play2.ToString()}");
}

}


class Spectacle : IDisposable
{
    private string? _title { get; set; }
    private string? _theater { get; set; }
    private string? _genre { get; set; }
    private int? _duration { get; set; } // Длительность в минутах
    private List<string>? _actors { get; set; }

    private bool _disposed = false;

    // Конструктор по умолчанию
    public Spectacle()
    {
        _title = "Без назви";
        _theater = "Невідомий театр";
        _genre = "Невідомий жанр";
        _duration = 0;
        _actors = new List<string>();
    }

    // Конструктор с параметрами
    public Spectacle(string? title, string? theater, string? genre, int? duration, List<string>? actors)
    {
        _title = title;
        _theater = theater;
        _genre = genre;
        _duration = duration;
        _actors = actors ?? new List<string>();
    }

    public override string ToString()
    {
        string actorsList = _actors != null && _actors.Count > 0 ? string.Join(", ", _actors) : "немає";
        return $"Вистава: \"{_title}\", Театр: {_theater}, Жанр: {_genre}, Тривалість: {_duration} хв., Актори: {actorsList}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Spectacle other)
        {
            return _title == other._title &&
                   _theater == other._theater &&
                   _genre == other._genre &&
                   _duration == other._duration &&
                   (_actors == null && other._actors == null || _actors != null && other._actors != null && _actors.SequenceEqual(other._actors));
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_title, _theater, _genre, _duration, _actors);
    }

    // Статический метод фабрики (Валидация уровня Senior)
    public static Spectacle Create(string title, string theater, string genre, int duration, List<string> actors)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва вистави не може бути порожньою.");

        if (string.IsNullOrWhiteSpace(theater))
            throw new ArgumentException("Назва театру не може бути порожньою.");

        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException("Жанр не може бути порожнім.");

        if (duration <= 0)
            throw new ArgumentOutOfRangeException(nameof(duration), "Тривалість вистави має бути більшою за 0 хвилин.");

        if (actors == null || actors.Count == 0)
            throw new ArgumentException("Список акторів не може бути порожнім.");

        return new Spectacle(title, theater, genre, duration, actors);
    }

    // Ввод данных из консоли с автоматической перезаписью при успехе
    public void input()
    {
        while (true)
        {
            try
            {
                Write("Введіть назву вистави: ");
                string title = ReadLine() ?? "";

                Write("Введіть назву театру: ");
                string theater = ReadLine() ?? "";

                Write("Введіть жанр: ");
                string genre = ReadLine() ?? "";

                Write("Введіть тривалість (у хвилинах): ");
                int duration = int.Parse(ReadLine() ?? "0");

                Write("Введіть акторів через кому: ");
                string actorsInput = ReadLine() ?? "";
                List<string> actors = actorsInput.Split(',')
                                                 .Select(a => a.Trim())
                                                 .Where(a => !string.IsNullOrWhiteSpace(a))
                                                 .ToList();

                // Запуск проверки через статический метод
                Spectacle validSpectacle = Spectacle.Create(title, theater, genre, duration, actors);

                _title = validSpectacle._title;
                _theater = validSpectacle._theater;
                _genre = validSpectacle._genre;
                _duration = validSpectacle._duration;
                _actors = validSpectacle._actors;

                break;
            }
            catch (Exception ex)
            {
                WriteLine($"\n[Помилка]: {ex.Message} Будь ласка, спробуйте ввести дані знову.\n");
            }
        }
    }

    // Паттерн IDisposable + Финализатор
    ~Spectacle()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this); // Работает напрямую благодаря твоему using static System.GC;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            WriteLine($"[Dispose]: Керовані ресурси вистави \"{_title}\" успішно звільнено з пам'яті.");
            _title = null;
            _theater = null;
            _genre = null;
            if (_actors != null)
            {
                _actors.Clear();
                _actors = null;
            }
        }
        else
        {
            WriteLine("[Фіналізатор]: Об'єкт вистави знищено автоматично збирачем сміття (GC).");
        }

        _disposed = true;
    }
}
