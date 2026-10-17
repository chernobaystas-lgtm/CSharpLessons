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

            // Завдання 8: кількість голосних літер у реченні
            Write("Введіть речення: ");
            string sentence = ReadLine()!.ToLower();

            // українські та англійські голосні
            string vowels = "аеєиіїоуюяaeiou";

            int count = 0;

            for (int i = 0; i < sentence.Length; i++)
            {
                if (vowels.IndexOf(sentence[i]) != -1)
                {
                    count++;
                }
            }

            WriteLine($"Кількість голосних літер: {count}");
        }
    }
}