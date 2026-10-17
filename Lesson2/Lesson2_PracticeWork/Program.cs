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

            // Завдання 3: кількість повторень послідовності з трьох чисел в масиві
            int[] array = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };

            Write("Масив: ");
            for (int i = 0; i < array.Length; i++)
            {
                Write(array[i] + " ");
            }
            WriteLine();

            int[] sequence = new int[3];

            for (int i = 0; i < sequence.Length; i++)
            {
                Write($"Введіть число {i + 1}: ");
                if (!int.TryParse(ReadLine(), out sequence[i]))
                {
                    WriteLine("Помилка: потрібно ввести ціле число!");
                    return;
                }
            }

            int count = 0;

            for (int i = 0; i <= array.Length - sequence.Length; i++)
            {
                if (array[i] == sequence[0] &&
                    array[i + 1] == sequence[1] &&
                    array[i + 2] == sequence[2])
                {
                    count++;
                }
            }

            WriteLine($"Кількість повторень послідовності: {count}");
        }
    }
}