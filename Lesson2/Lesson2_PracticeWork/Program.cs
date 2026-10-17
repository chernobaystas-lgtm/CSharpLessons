using System;
using System.Text;
using static System.Console;

namespace Lesson2_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 5: мінімальне та максимальне значення у двовимірному масиві
            Random random = new Random();
            int[,] array = new int[4, 5];

            WriteLine("Масив:");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(-50, 51);
                    Write($"{array[i, j],5}");
                }
                WriteLine();
            }

            int min = array[0, 0];
            int max = array[0, 0];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] < min) min = array[i, j];
                    if (array[i, j] > max) max = array[i, j];
                }
            }

            WriteLine($"Мінімальне значення: {min}");
            WriteLine($"Максимальне значення: {max}");
        }
    }
}