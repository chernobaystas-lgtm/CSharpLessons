using System;
using System.Text;
using static System.Console;

namespace Lesson1_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 6: лінія за заданими параметрами
            Write("Довжина лінії: ");
            int length = int.Parse(ReadLine()!);
            Write("Символ-заповнювач: ");
            char symbol = ReadLine()![0];
            Write("Напрямок (h - горизонтальна, v - вертикальна): ");
            string direction = ReadLine()!.ToLower();

            if (direction == "h")
            {
                WriteLine(new string(symbol, length));
            }
            else if (direction == "v")
            {
                for (int i = 0; i < length; i++)
                {
                    WriteLine(symbol);
                }
            }
            else
            {
                WriteLine("Невірний напрямок!");
            }
        }
    }
}