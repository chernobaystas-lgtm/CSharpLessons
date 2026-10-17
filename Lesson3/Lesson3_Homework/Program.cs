using System;
using System.Text;
using static System.Console;

namespace Lesson3_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 3: метод фільтрує масив за переданими параметрами
            int[] original = { 1, 2, 6, -1, 88, 7, 6 };
            int[] filter = { 6, 88, 7 };

            PrintArray("Оригінальний масив:", original);
            PrintArray("Масив для фільтрації:", filter);

            Filter(ref original, filter, out int removed);

            PrintArray("Отриманий масив після фільтрації:", original);
            WriteLine($"Кількість виконаних видалень елементів: {removed}");
        }

        // Видаляє з оригінального масиву всі елементи, що є в масиві для фільтрації.
        // ref потрібен, бо розмір масиву змінюється і метод підміняє сам масив.
        // В removed записується кількість видалених елементів.
        static void Filter(ref int[] original, int[] filter, out int removed)
        {
            removed = 0;

            // спочатку рахуємо, скільки елементів треба видалити
            for (int i = 0; i < original.Length; i++)
            {
                if (Contains(filter, original[i]))
                {
                    removed++;
                }
            }

            // потім збираємо новий масив без цих елементів
            int[] result = new int[original.Length - removed];
            int index = 0;

            for (int i = 0; i < original.Length; i++)
            {
                if (!Contains(filter, original[i]))
                {
                    result[index] = original[i];
                    index++;
                }
            }

            original = result;
        }

        // Перевіряє, чи є значення в масиві
        static bool Contains(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return true;
                }
            }

            return false;
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