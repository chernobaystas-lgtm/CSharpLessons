using System;
using System.Text;
using static System.Console;

namespace Lesson2_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 6: кількість слів у реченні
            Write("Введіть речення: ");
            string sentence = ReadLine()!;

            int count = 0;
            bool inWord = false;

            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] != ' ')
                {
                    if (!inWord)
                    {
                        count++;
                        inWord = true;
                    }
                }
                else
                {
                    inWord = false;
                }
            }

            WriteLine($"Кількість слів: {count}");
        }
    }
}