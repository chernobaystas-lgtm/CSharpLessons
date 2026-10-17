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

            // Завдання 2: метод перевіряє, чи є число числом Фібоначчі
            Write("Введіть невід'ємне ціле число: ");
            if (!int.TryParse(ReadLine(), out int number) || number < 0)
            {
                WriteLine("Помилка: потрібно ввести невід'ємне ціле число!");
                return;
            }

            int original = number;

            bool isFibonacci = IsFibonacci(ref number, out int index);

            if (isFibonacci)
            {
                WriteLine($"Число {original} є числом Фібоначчі, його порядковий номер: {index}");
            }
            else
            {
                WriteLine($"Число {original} не є числом Фібоначчі.");
                WriteLine($"Найближче число Фібоначчі: {number}, його порядковий номер: {index}");
            }
        }

        // Повертає true, якщо number є числом Фібоначчі, і записує в index його порядковий номер.
        // Інакше повертає false, замінює number на найближче за значенням число Фібоначчі
        // і записує в index порядковий номер цього числа.
        // Нумерація: F(0) = 0, F(1) = 1, F(2) = 1, F(3) = 2, F(4) = 3, F(5) = 5 ...
        static bool IsFibonacci(ref int number, out int index)
        {
            long previous = 0;  // F(k-1)
            long current = 0;   // F(k)
            long next = 1;      // F(k+1)
            int k = 0;

            // йдемо по послідовності, поки не дійдемо до числа, яке не менше за number
            while (current < number)
            {
                previous = current;
                current = next;
                next = previous + current;
                k++;
            }

            if (current == number)
            {
                index = k;
                return true;
            }

            // number лежить між previous і current, обираємо ближче
            // (якщо відстані однакові, беремо менше)
            if (number - previous <= current - number)
            {
                number = (int)previous;
                index = k - 1;
            }
            else
            {
                number = (int)current;
                index = k;
            }

            return false;
        }
    }
}