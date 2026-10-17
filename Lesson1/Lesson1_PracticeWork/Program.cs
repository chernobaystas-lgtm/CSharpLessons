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

            Write("Введіть шестизначне число: ");
            int number = int.Parse(ReadLine()!);

            if (number < 100000 || number > 999999)
            {
                WriteLine("Це не шестизначне число!");
                return;
            }

            Write("Результат: ");
            while (number > 0)
            {
                Write(number % 10);
                number /= 10;
            }
            WriteLine();
        }
    }
}