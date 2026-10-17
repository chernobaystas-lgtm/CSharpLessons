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

            // Завдання 9: кількість входжень підрядка в рядок
            Write("Користувач ввів: ");
            string sourceString = ReadLine()!;

            Write("Підрядок для пошуку: ");
            string searchWord = ReadLine()!;

            if (string.IsNullOrEmpty(searchWord))
            {
                WriteLine("Помилка: підрядок для пошуку не може бути порожнім!");
                return;
            }

            int count = 0;
            int index = 0;

            while ((index = sourceString.IndexOf(searchWord, index, StringComparison.Ordinal)) != -1)
            {
                count++;
                index += searchWord.Length;
            }

            WriteLine($"Результат пошуку: {count}");
        }
    }
}