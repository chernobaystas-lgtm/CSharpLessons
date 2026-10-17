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

            // Завдання 5: числа від A до B, кожне виводиться стільки разів, яке його значення
            Write("Введіть A: ");
            int a = int.Parse(ReadLine()!);
            Write("Введіть B: ");
            int b = int.Parse(ReadLine()!);

            if (a <= 0 || a >= b)
            {
                WriteLine("Потрібно: A > 0 та A < B");
                return;
            }

            for (int i = a; i <= b; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Write(i + " ");
                }
                WriteLine();
            }
        }
    }
}