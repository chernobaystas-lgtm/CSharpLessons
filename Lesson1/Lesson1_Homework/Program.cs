using System;
using System.Text;
using static System.Console;

namespace Lesson1_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 7: усі парні числа в діапазоні з нормалізацією меж
            Write("Введіть початок діапазону: ");
            if (!int.TryParse(ReadLine(), out int start))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            Write("Введіть кінець діапазону: ");
            if (!int.TryParse(ReadLine(), out int end))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
                WriteLine($"Межі вказано неправильно. Нормалізований діапазон: {start}-{end}");
            }

            Write($"Парні числа від {start} до {end}: ");
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Write(i + " ");
                }
            }
            WriteLine();
        }
    }
}