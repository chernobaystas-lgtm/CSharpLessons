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

            // Завдання 4: метод видаляє зайві пробіли та виділяє верхнім регістром слова,
            // у яких перший символ зустрічається більше одного разу
            Write("Введіть рядок: ");
            string text = ReadLine()!;

            string result = Normalize(text);

            WriteLine($"Результат: {result}");
        }

        // Видаляє зайві пробіли (на початку, в кінці та між словами залишає по одному).
        // Слова, що містять свій перший символ більше одного разу, записує великими літерами.
        static string Normalize(string text)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string result = "";

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                char first = char.ToLower(word[0]);
                int count = 0;

                for (int j = 0; j < word.Length; j++)
                {
                    if (char.ToLower(word[j]) == first)
                    {
                        count++;
                    }
                }

                if (count > 1)
                {
                    word = word.ToUpper();
                }

                if (i > 0)
                {
                    result += " ";
                }

                result += word;
            }

            return result;
        }
    }
}