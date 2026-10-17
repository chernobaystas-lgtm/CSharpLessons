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

            // Завдання 2: кількість значень в масиві менших за задане число
            Random random = new Random();
            int[] array = new int[10];

            Write("Масив: ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 21);
                Write(array[i] + " ");
            }
            WriteLine();

            Write("Введіть число: ");
            if (!int.TryParse(ReadLine(), out int limit))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            int count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < limit)
                {
                    count++;
                }
            }

            WriteLine($"Кількість значень менших, ніж {limit}: {count}");
        }
    }
}