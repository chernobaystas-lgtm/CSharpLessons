using System;
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

            // Завдання 6: перша літера кожного речення у верхній регістр
            Write("Введіть текст: ");
            string text = ReadLine()!;

            string result = "";
            bool newSentence = true;

            for (int i = 0; i < text.Length; i++)
            {
                char symbol = text[i];

                if (!char.IsWhiteSpace(symbol) && newSentence)
                {
                    symbol = char.ToUpper(symbol);
                    newSentence = false;
                }

                result += symbol;

                if (symbol == '.' || symbol == '!' || symbol == '?')
                {
                    newSentence = true;
                }
            }

            WriteLine($"Результат: {result}");
        }
    }
}