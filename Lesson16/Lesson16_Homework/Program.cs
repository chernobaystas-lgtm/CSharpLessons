namespace LessonsMainCSharp;
using System.Text.RegularExpressions;



class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Write("Введіть шлях до файлу з 100 000 чисел: ");
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
                string dir = fileInfo.DirectoryName ?? "";
                FileInfo positiveFile = new FileInfo(Path.Combine(dir, "Positive.txt"));
                FileInfo negativeFile = new FileInfo(Path.Combine(dir, "Negative.txt"));
                FileInfo twoDigitFile = new FileInfo(Path.Combine(dir, "TwoDigit.txt"));
                FileInfo fiveDigitFile = new FileInfo(Path.Combine(dir, "FiveDigit.txt"));

                int posCount = 0, negCount = 0, twoDigitCount = 0, fiveDigitCount = 0;

                using (StreamReader reader = new StreamReader(fileInfo.FullName))
                using (StreamWriter posWriter = positiveFile.CreateText())
                using (StreamWriter negWriter = negativeFile.CreateText())
                using (StreamWriter twoWriter = twoDigitFile.CreateText())
                using (StreamWriter fiveWriter = fiveDigitFile.CreateText())
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string token in tokens)
                        {
                            if (int.TryParse(token, out int num))
                            {
                                int absNum = Math.Abs(num);

                                if (num > 0) { posWriter.WriteLine(num); posCount++; }
                                else if (num < 0) { negWriter.WriteLine(num); negCount++; }

                                if (absNum >= 10 && absNum <= 99) { twoWriter.WriteLine(num); twoDigitCount++; }
                                else if (absNum >= 10000 && absNum <= 99999) { fiveWriter.WriteLine(num); fiveDigitCount++; }
                            }
                        }
                    }
                }

                WriteLine("\n=== Статистика роботи додатку ===");
                WriteLine($"1. Кількість додатних чисел: {posCount}");
                WriteLine($"2. Кількість від'ємних чисел: {negCount}");
                WriteLine($"3. Кількість двозначних чисел: {twoDigitCount}");
                WriteLine($"4. Кількість п'ятизначних чисел: {fiveDigitCount}");
                WriteLine($"\nФайли успішно створені в папці: {dir}");
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