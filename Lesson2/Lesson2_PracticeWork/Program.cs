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

            // Завдання 7: перевернути кожне слово речення
            Write("Введіть речення: ");
            string sentence = ReadLine()!;

            string result = "";
            string word = "";

            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] != ' ')
                {
                    // додаємо символ на початок слова, так слово розвертається
                    word = sentence[i] + word;
                }
                else
                {
                    result = result + word + " ";
                    word = "";
                }
            }

            result = result + word;

            WriteLine($"Результат: {result}");
        }
    }
}