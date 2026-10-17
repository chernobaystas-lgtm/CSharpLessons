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

            // Завдання 7: заміна неприпустимого слова на *** та статистика
            WriteLine("Введіть текст (порожній рядок - кінець введення):");

            string text = "";
            string line;

            while (!string.IsNullOrEmpty(line = ReadLine()!))
            {
                text += line + Environment.NewLine;
            }

            Write("Введіть неприпустиме слово: ");
            string banned = ReadLine()!;

            if (string.IsNullOrEmpty(banned))
            {
                WriteLine("Помилка: слово не може бути порожнім!");
                return;
            }

            string result = "";
            int count = 0;
            int position = 0;

            while (position < text.Length)
            {
                int found = text.IndexOf(banned, position, StringComparison.OrdinalIgnoreCase);

                if (found == -1)
                {
                    result += text.Substring(position);
                    break;
                }

                int end = found + banned.Length;

                // слово має бути окремим, а не частиною іншого слова
                bool startOk = found == 0 || !char.IsLetter(text[found - 1]);
                bool endOk = end >= text.Length || !char.IsLetter(text[end]);

                if (startOk && endOk)
                {
                    result += text.Substring(position, found - position) + "***";
                    count++;
                    position = end;
                }
                else
                {
                    result += text.Substring(position, found - position + 1);
                    position = found + 1;
                }
            }

            WriteLine();
            WriteLine("Результат роботи:");
            Write(result);
            WriteLine();
            WriteLine($"Статистика: {count} заміни слова {banned}.");
        }
    }
}