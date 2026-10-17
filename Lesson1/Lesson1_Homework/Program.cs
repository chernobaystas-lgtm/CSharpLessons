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

            // Завдання 3: створити число з чотирьох цифр
            int result = 0;

            for (int i = 1; i <= 4; i++)
            {
                Write($"Введіть цифру {i}: ");

                if (!int.TryParse(ReadLine(), out int digit) || digit < 0 || digit > 9)
                {
                    WriteLine("Помилка: потрібно ввести цифру від 0 до 9!");
                    return;
                }

                result = result * 10 + digit;
            }

            WriteLine($"Число: {result}");
        }
    }
}