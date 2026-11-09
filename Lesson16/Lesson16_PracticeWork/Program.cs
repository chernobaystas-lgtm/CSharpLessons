namespace LessonsMainCSharp;



class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Write("Введіть шлях до файлу с числами (0 для виходу): ");
            string? FilePath = ReadLine();

            if (FilePath == "0")
            {
                break;
            }

            if (!File.Exists(FilePath))
            {
                WriteLine("Файл не існує!\n");
                continue;
            }

            WriteLine("Файл існує!");

            Write("Введіть шлях до папки, куди будемо створювати файли: ");
            string? NewDirectoryPath =  ReadLine();

            if (string.IsNullOrWhiteSpace(NewDirectoryPath) || !Directory.Exists(NewDirectoryPath))
            {
                WriteLine("Папка не існує!\n");
                continue;
            }

            WriteLine("Папка існує!");
            WriteLine("Все файли будуть створені в папці: " + NewDirectoryPath);
            string EvenFilePath = Combine(NewDirectoryPath, "Even.txt");
            string OddFilePath = Combine(NewDirectoryPath, "Odd.txt");

            try
            {
using (StreamReader reader = new StreamReader(FilePath))
{
    using (StreamWriter evenWriter = new StreamWriter(EvenFilePath))
    {
        using (StreamWriter oddwriter = new StreamWriter(OddFilePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] tokens = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string token in tokens)
                {
                    if (int.TryParse(token, out int number))
                    {
                        if (number % 2 == 0)
                        {
                            evenWriter.WriteLine(number);
                            evenWriter.Flush();
                        }
                        else
                        {
                            oddwriter.WriteLine(number);
                            oddwriter.Flush();
                        }
                    }
                }
            }
        }
    }
}

            }
            catch (Exception ex)
            {
                WriteLine($"Помилка при роботі з файлами: {ex.Message}\n");
            }
        }

        ReadKey();
    }
}