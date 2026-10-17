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

            Write("Початок діапазону: ");
            long start = long.Parse(ReadLine()!);
            Write("Кінець діапазону: ");
            long end = long.Parse(ReadLine()!);

            long a = 0;
            long b = 1;

            while (a <= end)
            {
                if (a >= start)
                    Write(a + " ");

                long next = a + b;
                a = b;
                b = next;
            }
            WriteLine();
        }
    }
}