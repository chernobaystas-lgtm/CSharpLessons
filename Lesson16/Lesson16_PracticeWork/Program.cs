namespace LessonsMainCSharp;


class Program
{
  static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Write("Введіть шлях до файлу: ");
            string? filePath = ReadLine();

            if (!File.Exists(filePath))
            {
                WriteLine("Файл не існує!\n");
                continue;
            }

            Write("Введіть слово для пошуку: ");
            string? searchWord = ReadLine();

            if (string.IsNullOrWhiteSpace(searchWord))
            {
                WriteLine("Слово не може бути порожнім!\n");
                continue;
            }

            try
            {
                string text = ReadAllText(filePath);

                char[] charArray = searchWord.ToCharArray();
                Array.Reverse(charArray);
                string reversedWord = new string(charArray);

                int normalCount = Regex.Matches(text, @"\b" + Regex.Escape(searchWord) + @"\b", RegexOptions.IgnoreCase).Count;
                int reversedCount = Regex.Matches(text, @"\b" + Regex.Escape(reversedWord) + @"\b", RegexOptions.IgnoreCase).Count;

                if (normalCount > 0)
                {
                    WriteLine($"Слово '{searchWord}' знайдено.");
                    WriteLine($"Кількість входжень слова: {normalCount}");
                }
                else
                {
                    WriteLine($"Слово '{searchWord}' не знайдено.");
                }

                WriteLine($"Кількість входжень у зворотному порядку ('{reversedWord}'): {reversedCount}");
                break;
            }
            catch (Exception ex)
            {
                WriteLine($"Помилка при роботі з файлом: {ex.Message}\n");
            }
        }

        ReadKey();
    }
}


