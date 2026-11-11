using LessonsMainCSharp;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        string filePath = "magazine.json";

        Magazine mag = new Magazine();
        mag.Input();

        WriteLine("\n--- Введенные данные ---");
        WriteLine(mag);

        mag.SaveToJSON(filePath);
        WriteLine($"\nЖурнал успешно сохранен в файл {filePath}!");

        WriteLine("\n--- Загружено из JSON файла ---");
        Magazine.PrintFromJSON(filePath);
    }
}



public class Magazine
{
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public int PageCount { get; set; }

    public Magazine() { }

    public Magazine(string title, string publisher, DateOnly releaseDate, int pageCount)
    {
        Title = title;
        Publisher = publisher;
        ReleaseDate = releaseDate;
        PageCount = pageCount;
    }

    public void Input()
    {
        Write("Введите название журнала: ");
        Title = ReadLine() ?? string.Empty;

        Write("Введите название издательства: ");
        Publisher = ReadLine() ?? string.Empty;

        // 1. Исправлено: объявляем date до цикла
        Write("Введите дату выпуска (ГГГГ-ММ-ДД): ");
        DateOnly date;
        while (!DateOnly.TryParse(ReadLine(), out date))
        {
            Write("Неверный формат. Введите дату еще раз (например, 2026-11-11): ");
        }
        ReleaseDate = date;

        // 2. Исправлено: объявляем pages до цикла
        Write("Введите количество страниц: ");
        int pages;
        while (!int.TryParse(ReadLine(), out pages) || pages <= 0)
        {
            Write("Введите корректное число страниц: ");
        }
        PageCount = pages;
    }

    public override string ToString()
    {
        return $"Журнал: \"{Title}\" | Издательство: {Publisher} | Дата: {ReleaseDate} | Страниц: {PageCount}";
    }

    public string ToJson()
    {
        // 3. Исправлено: убран лишний using у JsonSerializerOptions
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }

    public static Magazine? FromJson(string json)
    {
        return JsonSerializer.Deserialize<Magazine>(json);
    }

    // 4. Исправлено: добавлен параметр string filePath в скобки
    public void SaveToJSON(string filePath)
    {
        string json = ToJson();

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(json);
        }
    }

    public static void PrintFromJSON(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            // 5. Исправлено: заменен ReadToEndAsync() на синхронный ReadToEnd()
            string json = reader.ReadToEnd();

            Magazine? mag = JsonSerializer.Deserialize<Magazine>(json);

            WriteLine(mag);
        }
    }
}
