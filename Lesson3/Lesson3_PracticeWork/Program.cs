using System;
using System.Text;
using static System.Console;

namespace Lesson3_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 3: метод знаходить суму елементів кожного рядка ступінчатого масиву
            // і додає ці суми як останній рядок масиву
            Random random = new Random();
            int rows = 4;
            int[][] array = new int[rows][];

            // кожен рядок має випадкову довжину від 2 до 5
            for (int i = 0; i < rows; i++)
            {
                array[i] = new int[random.Next(2, 6)];

                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = random.Next(1, 10);
                }
            }

            PrintJagged("Масив до:", array);

            int[][] result = AddRowSums(array);

            PrintJagged("Масив після (останній рядок це суми рядків):", result);
        }

        // Повертає новий ступінчатий масив: усі старі рядки + останній рядок із сумами
        static int[][] AddRowSums(int[][] array)
        {
            int[][] result = new int[array.Length + 1][];
            int[] sums = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];

                int sum = 0;
                for (int j = 0; j < array[i].Length; j++)
                {
                    sum += array[i][j];
                }

                sums[i] = sum;
            }

            result[array.Length] = sums;

            return result;
        }

        static void PrintJagged(string title, int[][] array)
        {
            WriteLine();
            WriteLine(title);

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Write($"{array[i][j],4}");
                }
                WriteLine();
            }
        }
    }
}