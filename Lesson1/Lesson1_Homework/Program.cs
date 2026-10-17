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

            // Завдання 4: поміняти місцями дві цифри шестизначного числа
            Write("Введіть шестизначне число: ");

            if (!int.TryParse(ReadLine(), out int number) || number < 100000 || number > 999999)
            {
                WriteLine("Помилка: потрібно ввести шестизначне число!");
                return;
            }

            Write("Введіть номер першого розряду (1-6): ");
            if (!int.TryParse(ReadLine(), out int first) || first < 1 || first > 6)
            {
                WriteLine("Помилка: номер розряду має бути від 1 до 6!");
                return;
            }

            Write("Введіть номер другого розряду (1-6): ");
            if (!int.TryParse(ReadLine(), out int second) || second < 1 || second > 6)
            {
                WriteLine("Помилка: номер розряду має бути від 1 до 6!");
                return;
            }

            char[] digits = number.ToString().ToCharArray();

            char temp = digits[first - 1];
            digits[first - 1] = digits[second - 1];
            digits[second - 1] = temp;

            WriteLine($"Число {number} перетворилося на {new string(digits)}");
        }
    }
}