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




public class Article
{
    public string Title { get; set; } = string.Empty;
    public int CharacterCount { get; set; }
    public string Announcement { get; set; } = string.Empty;

    public Article() { }

    public Article(string title, int characterCount, string announcement)
    {
        Title = title;
        CharacterCount = characterCount;
        Announcement = announcement;
    }

    public void Input()
    {
        Write("  Введите название статьи: ");
        Title = ReadLine() ?? string.Empty;

        Write("  Введите количество символов: ");
        int chars;
        while (!int.TryParse(ReadLine(), out chars) || chars <= 0)
        {
            Write("  Введите корректное число символов: ");
        }
        CharacterCount = chars;

        Write("  Введите анонс статьи: ");
        Announcement = ReadLine() ?? string.Empty;
    }

    public override string ToString()
    {
        return $"  - Статья: \"{Title}\" ({CharacterCount} симв.) | Анонс: {Announcement}";
    }
}

public class Magazine
{
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public int PageCount { get; set; }
    public List<Article> Articles { get; set; } = new();

    public Magazine() { }

    public Magazine(string title, string publisher, DateOnly releaseDate, int pageCount, List<Article> articles)
    {
        Title = title;
        Publisher = publisher;
        ReleaseDate = releaseDate;
        PageCount = pageCount;
        Articles = articles;
    }

    public void Input()
    {
        Write("Введите название журнала: ");
        Title = ReadLine() ?? string.Empty;

        Write("Введите название издательства: ");
        Publisher = ReadLine() ?? string.Empty;

        Write("Введите дату выпуска (ГГГГ-ММ-ДД): ");
        DateOnly date;
        while (!DateOnly.TryParse(ReadLine(), out date))
        {
            Write("Неверный формат. Введите дату еще раз (например, 2026-11-11): ");
        }
        ReleaseDate = date;

        Write("Введите количество страниц: ");
        int pages;
        while (!int.TryParse(ReadLine(), out pages) || pages <= 0)
        {
            Write("Введите корректное число страниц: ");
        }
        PageCount = pages;

        Write("Сколько статей добавить в журнал? ");
        int articleCount;
        while (!int.TryParse(ReadLine(), out articleCount) || articleCount < 0)
        {
            Write("Введите число (0 или больше): ");
        }

        Articles.Clear();
        for (int i = 0; i < articleCount; i++)
        {
            WriteLine($"\nСтатья #{i + 1}:");
            Article article = new Article();
            article.Input();
            Articles.Add(article);
        }
    }

    public override string ToString()
    {
        string result = $"Журнал: \"{Title}\" | Издательство: {Publisher} | Дата: {ReleaseDate} | Страниц: {PageCount}\n";
        result += "Список статей:\n";

        if (Articles.Count == 0)
        {
            result += "  (статей пока нет)";
        }
        else
        {
            foreach (var article in Articles)
            {
                result += article.ToString() + "\n";
            }
        }

        return result.TrimEnd();
    }

    public string ToJson()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }

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
            string json = reader.ReadToEnd();

            Magazine? mag = JsonSerializer.Deserialize<Magazine>(json);

            WriteLine(mag);
        }
    }
}