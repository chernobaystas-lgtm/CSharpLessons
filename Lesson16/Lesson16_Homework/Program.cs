namespace LessonsMainCSharp;
using System.Text.RegularExpressions;



class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Clear();
            WriteLine("=== Головне меню ===");
            WriteLine("1. Завдання 1 (Генерація простих чисел та Фібоначчі)");
            WriteLine("2. Завдання 2 (Пошук та заміна слова у файлі)");
            WriteLine("3. Завдання 3 (Програма 'Модератор')");
            WriteLine("0. Вихід");
            Write("\nВиберіть номер завдання: ");

            string? choice = ReadLine();

            if (choice == "0") break;

            if (choice == "1")
            {
                RunTask1();
            }
            else if (choice == "2")
            {
                RunTask2();
            }
            else if (choice == "3")
            {
                RunTask3();
            }
            else
            {
                WriteLine("Невірний вибір! Натисніть будь-яку клавішу...");
                ReadKey();
            }
        }
    }

    static void RunTask1()
    {
        Clear();
        DirectoryInfo currentDir = new DirectoryInfo(AppContext.BaseDirectory);
        FileInfo primeFile = new FileInfo(Path.Combine(currentDir.FullName, "Primes.txt"));
        FileInfo fibonacciFile = new FileInfo(Path.Combine(currentDir.FullName, "Fibonacci.txt"));

        Random random = new Random();
        int[] numbers = new int[100];

        for (int i = 0; i < 100; i++)
        {
            numbers[i] = random.Next(1, 1001);
        }

        int primeCount = 0;
        int fibCount = 0;

        using (StreamWriter primeWriter = primeFile.CreateText())
        using (StreamWriter fibWriter = fibonacciFile.CreateText())
        {
            foreach (int num in numbers)
            {
                if (IsPrime(num))
                {
                    primeWriter.WriteLine(num);
                    primeCount++;
                }

                if (IsFibonacci(num))
                {
                    fibWriter.WriteLine(num);
                    fibCount++;
                }
            }
        }

        WriteLine("=== Статистика роботи додатку ===");
        WriteLine("Всього згенеровано чисел: 100");
        WriteLine($"Знайдено та збережено простих чисел: {primeCount}");
        WriteLine($"Знайдено та збережено чисел Фібоначчі: {fibCount}");
        WriteLine($"Файли збережено в: {currentDir.FullName}");
        WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
        ReadKey();
    }

    static void RunTask2()
    {
        Clear();
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
                WriteLine($"Помилка при роботі з файлом: {ex.Message}\n");
            }
        }
        WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
        ReadKey();
    }

    static void RunTask3()
    {
        Clear();
        while (true)
        {
            Write("Введіть шлях до файлу з текстом: ");
            string? textPath = ReadLine();

            if (string.IsNullOrWhiteSpace(textPath))
            {
                WriteLine("Шлях не може бути порожнім!\n");
                continue;
            }

            FileInfo textFile = new FileInfo(textPath);

            if (!textFile.Exists)
            {
                WriteLine("Файл з текстом не існує!\n");
                continue;
            }

            Write("Введіть шлях до файлу зі словами для модерації: ");
            string? wordsPath = ReadLine();

            if (string.IsNullOrWhiteSpace(wordsPath))
            {
                WriteLine("Шлях не може бути порожнім!\n");
                continue;
            }

            FileInfo wordsFile = new FileInfo(wordsPath);

            if (!wordsFile.Exists)
            {
                WriteLine("Файл зі словами не існує!\n");
                continue;
            }

            try
            {
                string[] badWords = File.ReadAllLines(wordsFile.FullName);
                string mainText = File.ReadAllText(textFile.FullName);
                int totalReplacements = 0;

                foreach (string badWord in badWords)
                {
                    string trimmedWord = badWord.Trim();
                    if (string.IsNullOrEmpty(trimmedWord)) continue;

                    string pattern = @"\b" + Regex.Escape(trimmedWord) + @"\b";
                    int matchesCount = Regex.Matches(mainText, pattern, RegexOptions.IgnoreCase).Count;

                    if (matchesCount > 0)
                    {
                        string mask = new string('*', trimmedWord.Length);
                        mainText = Regex.Replace(mainText, pattern, mask, RegexOptions.IgnoreCase);
                        totalReplacements += matchesCount;
                    }
                }

                File.WriteAllText(textFile.FullName, mainText);

                WriteLine("\n=== Статистика роботи додатку ===");
                WriteLine("Модерація завершена успішно.");
                WriteLine($"Всього замінено заборонених слів: {totalReplacements}");
                break;
            }
            catch (Exception ex)
            {
                WriteLine($"Помилка при роботі з файлами: {ex.Message}\n");
            }
        }
        WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
        ReadKey();
    }

    static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    static bool IsFibonacci(int number)
    {
        return IsPerfectSquare(5 * number * number + 4) || IsPerfectSquare(5 * number * number - 4);
    }

    static bool IsPerfectSquare(int x)
    {
        int s = (int)Math.Sqrt(x);
        return s * s == x;
    }
}
