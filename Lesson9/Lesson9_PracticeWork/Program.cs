using System.Text;
using System.Text.Unicode;
using static System.Console;


public static class StringExtensions
{
    public static int CountMatchingEndsWords(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        char[] separators = { ' ', ',', '.', '!', '?', '-', ';', ':', '\r', '\n' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        int count = 0;
        foreach (string word in words)
        {
            if (word.Length == 1) count++;
            else if (word.Length > 1)
            {
                char firstChar = char.ToLower(word[0]);
                char lastChar = char.ToLower(word[word.Length - 1]);

                if (firstChar == lastChar) count++;
            }
        }
        return count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        Write("Введите текст для проверки: ");
        string testText = ReadLine();

        int result = testText.CountMatchingEndsWords();
        WriteLine($"Количество подходящих слов: {result}");
    }
}