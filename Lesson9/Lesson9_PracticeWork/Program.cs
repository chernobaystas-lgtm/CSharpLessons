using System.Text;
using System.Text.Unicode;
using static System.Console;


class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        Predicate<int> isPrime = num =>
        {
            if (num < 2) return false;
            for (int i = 2; (long)i * i <= num; i++)
            {
                if (num % i == 0) return false;
            }
            return true;
        };

        Func<DateTime, DateTime, int> getDaysBetween = (date1, date2) =>
        {
            TimeSpan difference = date1 - date2;
            return Math.Abs(difference.Days);
        };

        Write("Введите число для проверки на простоту: ");
        int number = Convert.ToInt32(ReadLine());
        WriteLine($"Число {number} простое: {isPrime(number)}");

        DateTime d1 = new DateTime(2026, 10, 1);
        DateTime d2 = new DateTime(2026, 10, 23);
        WriteLine($"Дней между {d1.ToShortDateString()} и {d2.ToShortDateString()}: {getDaysBetween(d1, d2)}");
    }
}
