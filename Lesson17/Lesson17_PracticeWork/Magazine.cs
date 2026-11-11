
public class Magazine
{
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public List<Article> Articles { get; set; } = new();

    public int PageCount
    {
        get
        {
            if (Articles == null || Articles.Count == 0)
                return 0;

            int totalChars = 0;
            foreach (var article in Articles)
            {
                totalChars += article.CharacterCount;
            }

            return (int)Math.Ceiling(totalChars / 1800.0);
        }
    }

    public Magazine() { }

    public Magazine(string title, string publisher, DateOnly releaseDate, List<Article> articles)
    {
        Title = title;
        Publisher = publisher;
        ReleaseDate = releaseDate;
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
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
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
