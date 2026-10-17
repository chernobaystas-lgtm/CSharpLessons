using System;
using System.Globalization;
using System.Text;
using static System.Console;

namespace Lesson2_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 5: обчислення арифметичного виразу з операціями + і -
            Write("Введіть вираз (наприклад, 12 + 5 - 3): ");
            string expression = ReadLine()!.Replace(" ", "").Replace(',', '.');

            double result = 0;
            char operation = '+';
            string number = "";

            // проходимо на один символ далі кінця рядка, щоб обробити останнє число
            for (int i = 0; i <= expression.Length; i++)
            {
                char symbol = i < expression.Length ? expression[i] : '\0';

                if (symbol == '+' || symbol == '-' || symbol == '\0')
                {
                    if (number == "")
                    {
                        // знак на самому початку виразу, наприклад -5 + 3
                        if (i == 0 && symbol != '\0')
                        {
                            operation = symbol;
                            continue;
                        }

                        WriteLine("Помилка: невірний вираз!");
                        return;
                    }

                    if (!double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                    {
                        WriteLine("Помилка: невірне число у виразі!");
                        return;
                    }

                    if (operation == '+')
                    {
                        result += value;
                    }
                    else
                    {
                        result -= value;
                    }

                    operation = symbol;
                    number = "";
                }
                else if (char.IsDigit(symbol) || symbol == '.')
                {
                    number += symbol;
                }
                else
                {
                    WriteLine($"Помилка: недопустимий символ '{symbol}'. Дозволені тільки цифри, + і -");
                    return;
                }
            }

            WriteLine($"Результат: {result}");
        }
    }
}