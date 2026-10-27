using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson10_Homework;

record DayTemperature(int Day, double High, double Low)
{
    // Різниця між найвищою і найнижчою температурою
    public double Difference
    {
        get { return High - Low; }
    }
}

static class TemperatureExtensions
{
    // Повертає день з максимальною різницею між найвищою і найнижчою температурою.
    // Якщо таких днів кілька, повертає перший.
    public static DayTemperature MaxDifferenceDay(this DayTemperature[] days)
    {
        DayTemperature best = days[0];

        for (int i = 1; i < days.Length; i++)
        {
            if (days[i].Difference > best.Difference)
            {
                best = days[i];
            }
        }

        return best;
    }
}

class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        int count = ReadCount("Скільки днів: ");
        DayTemperature[] days = new DayTemperature[count];

        for (int i = 0; i < days.Length; i++)
        {
            WriteLine($"День {i + 1}:");

            double high = ReadNumber("  Найвища температура: ");
            double low;

            while (true)
            {
                low = ReadNumber("  Найнижча температура: ");

                if (low <= high)
                {
                    break;
                }

                WriteLine("  Помилка: найнижча не може бути вищою за найвищу.");
            }

            days[i] = new DayTemperature(i + 1, high, low);
        }

        WriteLine();
        WriteLine("День    Найвища   Найнижча   Різниця");

        for (int i = 0; i < days.Length; i++)
        {
            DayTemperature day = days[i];

            WriteLine($"{day.Day,3}{day.High,11:F1}{day.Low,11:F1}{day.Difference,10:F1}");
        }

        // викликаємо extension method так, ніби він є в самого масиву
        DayTemperature result = days.MaxDifferenceDay();

        WriteLine();
        WriteLine($"Максимальна різниця температур у день {result.Day}: {result.Difference:F1} " +
                  $"(від {result.Low:F1} до {result.High:F1})");
    }

    static int ReadCount(string text)
    {
        while (true)
        {
            Write(text);

            if (int.TryParse(ReadLine(), out int value) && value > 0)
            {
                return value;
            }

            WriteLine("Треба ввести ціле число більше нуля, ще раз.");
        }
    }

    static double ReadNumber(string text)
    {
        while (true)
        {
            Write(text);
            string input = (ReadLine() ?? "").Replace(',', '.');

            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }

            WriteLine("Треба ввести число, ще раз.");
        }
    }
}