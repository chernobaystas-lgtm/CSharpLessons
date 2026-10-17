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

            // Завдання 1: метод, що повертає добуток чисел у вказаному діапазоні
            Write("Введіть початок діапазону: ");
            if (!int.TryParse(ReadLine(), out int from))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            Write("Введіть кінець діапазону: ");
            if (!int.TryParse(ReadLine(), out int to))
            {
                WriteLine("Помилка: потрібно ввести ціле число!");
                return;
            }

            long result = Product(from, to);

            WriteLine($"Добуток чисел від {from} до {to}: {result}");
        }

        // Повертає добуток усіх цілих чисел від from до to включно.
        // Якщо межі вказані в неправильному порядку, міняє їх місцями.
        static long Product(int from, int to)
        {
            if (from > to)
            {
                int temp = from;
                from = to;
                to = temp;
            }

            long product = 1;

            for (int i = from; i <= to; i++)
            {
                product *= i;
            }

            return product;
        }
    }
}