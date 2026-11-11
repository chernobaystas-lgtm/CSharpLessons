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