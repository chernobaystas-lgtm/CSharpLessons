using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson1_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 2: підрахувати відсоток від числа
            Write("Введіть число: ");
            string firstInput = ReadLine()!.Replace(',', '.');

            Write("Введіть відсоток: ");
            string secondInput = ReadLine()!.Replace(',', '.');

            if (!double.TryParse(firstInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) ||
                !double.TryParse(secondInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double percent))
            {
                WriteLine("Помилка: потрібно ввести числа!");
                return;
            }

            double result = value * percent / 100;

            WriteLine($"{percent}% від {value} = {result}");
        }
    }
}