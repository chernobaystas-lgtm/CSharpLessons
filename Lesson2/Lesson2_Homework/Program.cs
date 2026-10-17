using System;
using System.Text;
using static System.Console;

namespace Lesson2_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 2: сума елементів між мінімальним і максимальним елементами
            Random random = new Random();
            int[,] array = new int[5, 5];

            WriteLine("Масив:");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    array[i, j] = random.Next(-100, 101);
                    Write($"{array[i, j],5}");
                }
                WriteLine();
            }

            // шукаємо позиції мінімуму і максимуму, рахуючи елементи по рядках
            int min = array[0, 0];
            int max = array[0, 0];
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int index = i * 5 + j;

                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                        minIndex = index;
                    }

                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                        maxIndex = index;
                    }
                }
            }

            int from = Math.Min(minIndex, maxIndex);
            int to = Math.Max(minIndex, maxIndex);

            int sum = 0;

            for (int index = from + 1; index < to; index++)
            {
                sum += array[index / 5, index % 5];
            }

            WriteLine();
            WriteLine($"Мінімум: {min} (позиція {minIndex + 1})");
            WriteLine($"Максимум: {max} (позиція {maxIndex + 1})");
            WriteLine($"Сума елементів між ними: {sum}");
        }
    }
}