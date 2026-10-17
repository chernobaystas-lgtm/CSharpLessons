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

            // Завдання 5: метод об'єднує масив цілих чисел (перший параметр)
            // і послідовність цілих чисел, що йдуть після масиву
            Random random = new Random();
            int[] array = new int[5];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 10);
            }

            PrintArray("Початковий масив:", array);

            // після масиву можна передати будь-яку кількість чисел
            int[] result1 = Merge(array, 100, 200, 300);
            PrintArray("Merge(array, 100, 200, 300):", result1);

            int[] result2 = Merge(array, 7);
            PrintArray("Merge(array, 7):", result2);

            // без додаткових чисел повертається копія масиву
            int[] result3 = Merge(array);
            PrintArray("Merge(array):", result3);
        }

        // Перший параметр це масив, після нього йде послідовність чисел (params).
        // Повертає новий масив: спочатку елементи масиву, потім числа послідовності.
        static int[] Merge(int[] array, params int[] numbers)
        {
            int[] result = new int[array.Length + numbers.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                result[array.Length + i] = numbers[i];
            }

            return result;
        }

        static void PrintArray(string title, int[] array)
        {
            WriteLine(title);

            for (int i = 0; i < array.Length; i++)
            {
                Write(array[i] + " ");
            }

            WriteLine();
            WriteLine();
        }
    }
}