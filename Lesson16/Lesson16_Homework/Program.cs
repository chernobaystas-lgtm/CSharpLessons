namespace LessonsMainCSharp;
using System.Text.RegularExpressions;



class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

      while (true)
{
    Write("Введіть шлях до файлу: ");
    string? inputPath = ReadLine();

    if (string.IsNullOrWhiteSpace(inputPath))
    {
        WriteLine("Шлях не може бути порожнім!\n");
        continue;
    }

    FileInfo fileInfo = new FileInfo(inputPath);

    if (!fileInfo.Exists)
    {
        WriteLine("Файл не існує!\n");
        continue;
    }

    Write("Введіть слово для пошуку: ");
    string? searchWord = ReadLine();

    Write("Введіть слово для заміни: ");
    string? replaceWord = ReadLine();

    if (string.IsNullOrWhiteSpace(searchWord) || replaceWord == null)
    {
        WriteLine("Некоректні слова для заміни!\n");
        continue;
    }

    try
    {
        string text = File.ReadAllText(fileInfo.FullName);

        string pattern = @"\b" + Regex.Escape(searchWord) + @"\b";
        int matchesCount = Regex.Matches(text, pattern, RegexOptions.IgnoreCase).Count;

        if (matchesCount > 0)
        {
            string updatedText = Regex.Replace(text, pattern, replaceWord, RegexOptions.IgnoreCase);
            File.WriteAllText(fileInfo.FullName, updatedText);

            WriteLine("\n=== Статистика роботи додатку ===");
            WriteLine($"Слово '{searchWord}' успішно замінено на '{replaceWord}'.");
            WriteLine($"Всього знайдено та змінено входжень: {matchesCount}");
        }
        else
        {
            WriteLine($"\nСлово '{searchWord}' у файлі не знайдено. Замін не відбулося.");
        }

        break;
    }
    catch (Exception ex)
    {
        WriteLine($"Помилка при роботе з файлом: {ex.Message}\n");
    }
}

ReadKey();

    }
}
