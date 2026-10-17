using System;
using System.Text;
using static System.Console;

namespace Lesson4_Homework
{
    class Program
    {
        // Цифри для систем числення до 16 включно
        const string Digits = "0123456789ABCDEF";

        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 1: калькулятор для переведення числа з однієї системи числення в іншу
            WriteLine("Оберіть напрямок конвертації:");
            WriteLine("1 - з десяткової в двійкову");
            WriteLine("2 - з двійкової в десяткову");
            WriteLine("3 - з десяткової у вісімкову");
            WriteLine("4 - з вісімкової в десяткову");
            WriteLine("5 - з десяткової в шістнадцяткову");
            WriteLine("6 - з шістнадцяткової в десяткову");
            Write("Ваш вибір: ");
            string choice = ReadLine()!;

            int fromBase;
            int toBase;

            switch (choice)
            {
                case "1": fromBase = 10; toBase = 2; break;
                case "2": fromBase = 2; toBase = 10; break;
                case "3": fromBase = 10; toBase = 8; break;
                case "4": fromBase = 8; toBase = 10; break;
                case "5": fromBase = 10; toBase = 16; break;
                case "6": fromBase = 16; toBase = 10; break;
                default:
                    WriteLine("Помилка: потрібно ввести число від 1 до 6!");
                    return;
            }

            Write($"Введіть число (система числення {fromBase}): ");
            string input = ReadLine()!.Trim();

            try
            {
                // спочатку переводимо в десяткову (тип int), потім у потрібну систему
                int value = FromBase(input, fromBase);
                string result = ToBase(value, toBase);

                WriteLine($"Результат: {result} (система числення {toBase})");
            }
            catch (FormatException)
            {
                WriteLine($"Помилка: у числі є символи, яких не може бути в системі числення {fromBase}!");
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

        // Переводить число з десяткової системи в систему з основою toBase
        static string ToBase(int value, int toBase)
        {
            if (value == 0)
            {
                return "0";
            }

            string result = "";

            while (value > 0)
            {
                // остача від ділення це наступна цифра, додаємо її на початок
                result = Digits[value % toBase] + result;
                value /= toBase;
            }

            return result;
        }

        // Переводить рядок із системи з основою fromBase в десяткове число (int).
        // Кидає виняток, якщо ввід неправильний:
        // ArgumentException - порожній рядок,
        // FormatException - символ, якого не може бути в цій системі,
        // OverflowException - число виходить за межі int.
        static int FromBase(string number, int fromBase)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentException("Порожній рядок");
            }

            int result = 0;

            for (int i = 0; i < number.Length; i++)
            {
                int digit = Digits.IndexOf(char.ToUpper(number[i]));

                if (digit == -1 || digit >= fromBase)
                {
                    throw new FormatException();
                }

                // checked сам кидає OverflowException, якщо число вийшло за межі int
                result = checked(result * fromBase + digit);
            }

            return result;
        }
    }
}