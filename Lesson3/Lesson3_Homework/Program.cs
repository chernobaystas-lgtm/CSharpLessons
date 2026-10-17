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

            // Завдання 1: метод відображає квадрат із символу
            Write("Введіть довжину сторони квадрата: ");
            if (!int.TryParse(ReadLine(), out int side) || side <= 0)
            {
                WriteLine("Помилка: потрібно ввести ціле число більше нуля!");
                return;
            }

            Write("Введіть символ: ");
            string input = ReadLine()!;

            if (string.IsNullOrEmpty(input))
            {
                WriteLine("Помилка: потрібно ввести символ!");
                return;
            }

            DrawSquare(side, input[0]);
        }

        // Виводить квадрат зі стороною side, заповнений символом symbol
        static void DrawSquare(int side, char symbol)
        {
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    Write(symbol);
                }
                WriteLine();
            }
        }
    }
}