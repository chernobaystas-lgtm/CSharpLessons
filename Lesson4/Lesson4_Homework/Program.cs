using System;
using System.Text;
using static System.Console;

namespace Lesson4_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 7: обчислення виразу з цілих чисел і оператора * (наприклад, 3*2*1*4)
            Write("Введіть вираз (наприклад, 3*2*1*4): ");
            string input = ReadLine()!.Replace(" ", "");

            try
            {
                int result = Multiply(input);

                WriteLine($"Результат: {result}");
            }
            catch (FormatException)
            {
                WriteLine("Помилка: невірний вираз! Дозволені тільки цілі числа та оператор *");
            }
            catch (OverflowException)
            {
                WriteLine("Помилка: число або результат виходить за межі діапазону типу int!");
            }
        }

        // Обчислює добуток чисел у виразі виду "число*число*число".
        // Кидає виняток, якщо вираз неправильний:
        // FormatException - порожній вираз, зайвий або недопустимий символ, пропущене число,
        // OverflowException - число або добуток виходить за межі int.
        static int Multiply(string expression)
        {
            // розбиваємо вираз на числа по символу *
            string[] parts = expression.Split('*');

            int result = 1;

            for (int i = 0; i < parts.Length; i++)
            {
                // int.Parse сам кидає FormatException (порожня частина, літери, інші оператори)
                // та OverflowException (занадто велике число)
                int value = int.Parse(parts[i]);

                // checked кидає OverflowException, якщо добуток вийшов за межі int
                result = checked(result * value);
            }

            return result;
        }
    }
}