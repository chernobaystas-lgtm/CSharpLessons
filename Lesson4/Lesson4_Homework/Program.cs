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

            // Завдання 2: перетворити рядок із символів 0-9 на ціле число (int)
            Write("Введіть набір символів 0-9: ");
            string input = ReadLine()!;

            try
            {
                int number = StringToInt(input);

                WriteLine($"Результат: {number}");
            }
            catch (FormatException)
            {
                WriteLine("Помилка: у рядку можуть бути тільки символи від 0 до 9!");
            }
            catch (OverflowException)
            {
                WriteLine("Помилка: число виходить за межі діапазону типу int!");
            }
            catch (ArgumentException)
            {
                WriteLine("Помилка: введено порожній рядок!");
            }
        }

        // Перетворює рядок із цифр 0-9 на число типу int.
        // Кидає виняток, якщо ввід неправильний:
        // ArgumentException - порожній рядок,
        // FormatException - символ, який не є цифрою,
        // OverflowException - число виходить за межі int.
        static int StringToInt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Порожній рядок");
            }

            int result = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    throw new FormatException();
                }

                // код символу '5' мінус код символу '0' дає саме число 5
                int digit = text[i] - '0';

                // checked сам кидає OverflowException, якщо число вийшло за межі int
                result = checked(result * 10 + digit);
            }

            return result;
        }
    }
}