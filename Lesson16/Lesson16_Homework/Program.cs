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

            try
            {
                string text = File.ReadAllText(fileInfo.FullName);

                char[] charArray = text.ToCharArray();
                Array.Reverse(charArray);
                string reversedText = new string(charArray);

                string newPath = Path.Combine(fileInfo.DirectoryName ?? "", "Reversed_" + fileInfo.Name);
                File.WriteAllText(newPath, reversedText);

                WriteLine($"\nВміст перевернуто. Створено новий файл: {newPath}");
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