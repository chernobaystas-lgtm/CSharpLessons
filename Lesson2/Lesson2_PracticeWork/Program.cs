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

            // Завдання 1: кількість парних, непарних та унікальних елементів масиву
            Random random = new Random();
            int[] array = new int[10];

            Write("Масив: ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 21);
                Write(array[i] + " ");
            }
            WriteLine();

            int evenCount = 0;
            int oddCount = 0;
            int uniqueCount = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0)
                {
                    evenCount++;
                }
                else
                {
                    oddCount++;
                }

                int occurrences = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i] == array[j])
                    {
                        occurrences++;
                    }
                }

                if (occurrences == 1)