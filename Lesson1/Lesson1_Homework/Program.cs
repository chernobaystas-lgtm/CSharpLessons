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

            // Завдання 1: Fizz Buzz для числа від 1 до 100
            Write("Введіть число від 1 до 100: ");

            if (!int.TryParse(ReadLine(), out int number) || number < 1 || number > 100)
            {
                WriteLine("Помилка: число має бути в діапазоні від 1 до 100!");
                return;
            }

            if (number % 3 == 0 && number % 5 == 0)
            {
                WriteLine("Fizz Buzz");
            }
            else if (number % 3 == 0)
            {
                WriteLine("Fizz");
            }
            else if (number % 5 == 0)
            {
                WriteLine("Buzz");
            }
            else
            {
                WriteLine(number);
            }
        }
    }
}