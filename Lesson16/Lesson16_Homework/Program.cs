namespace LessonsMainCSharp;



class Program
{
static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        DirectoryInfo currentDir = new DirectoryInfo(AppContext.BaseDirectory);

        FileInfo primeFile = new FileInfo(Combine(currentDir.FullName, "Primes.txt"));
        FileInfo fibonacciFile = new FileInfo(Combine(currentDir.FullName, "Fibonacci.txt"));

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
        WriteLine($"Всього згенеровано чисел: 100");
        WriteLine($"Знайдено та збережено простих чисел: {primeCount}");
        WriteLine($"Знайдено та збережено чисел Фібоначчі: {fibCount}");
        WriteLine($"Файли збережено в: {currentDir.FullName}");

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