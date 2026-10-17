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

            // Завдання 2: метод перевіряє, чи є число паліндромом
            Write("Введіть число: ");
            if (!int.TryParse(ReadLine(), out int number) || number < 0)
            {
                WriteLine("Помилка: потрібно ввести невід'ємне ціле число!");
                return;
            }

            if (IsPalindrome(number))
            {
                WriteLine($"Число {number} є паліндромом.");
            }
            else
            {
                WriteLine($"Число {number} не є паліндромом.");
            }
        }

        // Повертає true, якщо число читається однаково в обох напрямках
        static bool IsPalindrome(int number)
        {
            int original = number;
            int reversed = 0;

            while (number > 0)
            {
                reversed = reversed * 10 + number % 10;
                number /= 10;
            }

            return original == reversed;
        }
    }
}