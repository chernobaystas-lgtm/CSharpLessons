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

            // Завдання 4: обчислення логічного виразу, наприклад 3>2 або 7<3
            Write("Введіть логічний вираз (наприклад, 3>2): ");
            string input = ReadLine()!.Replace(" ", "");

            try
            {
                bool result = Evaluate(input);

                WriteLine($"Результат: {result.ToString().ToLower()}");
            }
            catch (FormatException)
            {
                WriteLine("Помилка: невірний вираз! Потрібно: число, оператор (<, >, <=, >=, ==, !=), число.");
            }
            catch (OverflowException)
            {
                WriteLine("Помилка: число виходить за межі діапазону типу int!");
            }
        }

        // Обчислює вираз виду "число оператор число".
        // Кидає виняток, якщо вираз неправильний:
        // FormatException - немає оператора, невідомий оператор або в числах є сторонні символи,
        // OverflowException - число виходить за межі int.
        static bool Evaluate(string expression)
        {
            // шукаємо перший символ оператора; з індексу 0 не починаємо,
            // щоб мінус на початку (від'ємне число) не плутався з оператором
            int position = -1;

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (c == '<' || c == '>' || c == '=' || c == '!')
                {
                    position = i;
                    break;
                }
            }

            if (position == -1)
            {
                throw new FormatException();
            }

            // оператор складається з 1 або 2 символів
            string op = expression[position].ToString();

            if (position + 1 < expression.Length && expression[position + 1] == '=')
            {
                op += "=";
            }

            string leftText = expression.Substring(0, position);
            string rightText = expression.Substring(position + op.Length);

            // int.Parse сам кидає FormatException та OverflowException
            int left = int.Parse(leftText);
            int right = int.Parse(rightText);

            switch (op)
            {
                case "<": return left < right;
                case ">": return left > right;
                case "<=": return left <= right;
                case ">=": return left >= right;
                case "==": return left == right;
                case "!=": return left != right;
                default: throw new FormatException(); // одиночні "=" або "!"
            }
        }
    }
}