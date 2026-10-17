using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson1_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 2: сума, максимум, мінімум і добуток п'яти чисел
            double[] numbers = new double[5];

            for (int i = 0; i < numbers.Length; i++)
            {
                Write($"Введіть число {i + 1}: ");
                string input = ReadLine()!.Replace(',', '.');
                numbers[i] = double.Parse(input, CultureInfo.InvariantCulture);
            }

            double sum = 0;
            double product = 1;
            double max = numbers[0];
            double min = numbers[0];

            foreach (double number in numbers)
            {
                sum += number;
                product *= number;

                if (number > max) max = number;
                if (number < min) min = number;
            }

            WriteLine($"Сума: {sum}");
            WriteLine($"Максимум: {max}");
            WriteLine($"Мінімум: {min}");
            WriteLine($"Добуток: {product}");
        }
    }
}