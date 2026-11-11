using LessonsMainCSharp;

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;


        string filePath = "magazines.json";

        Write("Сколько журналов вы хотите создать? ");
        int count;
        while (!int.TryParse(ReadLine(), out count) || count <= 0)
        {
            Write("Введите корректное число (больше 0): ");
        }

        Magazine[] magazines = new Magazine[count];

        for (int i = 0; i < count; i++)
        {
            WriteLine($"\n=== Ввод данных для журнала #{i + 1} ===");
            magazines[i] = new Magazine();
            magazines[i].Input();
        }

        WriteLine("\n ВВЕДЕННЫЕ ЖУРНАЛЫ ");
        foreach (var mag in magazines)
        {
            WriteLine(mag);
        }

        Magazine.SaveArrayToJSON(magazines, filePath);
        WriteLine($"\nМассив журналов успешно сохранен в файл {filePath}!");

        WriteLine("\n ЗАГРУЖЕНО ИЗ ФАЙЛА ");
        Magazine.PrintArrayFromJSON(filePath);
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

    public static void SaveArrayToJSON(Magazine[] magazines, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(magazines, options);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(json);
        }
    }

    public static void PrintArrayFromJSON(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string json = reader.ReadToEnd();
            Magazine[]? magazines = JsonSerializer.Deserialize<Magazine[]>(json);

            if (magazines != null)
            {
                foreach (var mag in magazines)
                {
                    WriteLine(mag);
                }
            }
        }
    }
}