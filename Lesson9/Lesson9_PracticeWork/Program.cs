using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
        public delegate void delPrintMessage(string mes);
         static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;
          
            Write("Write a number to pow it: ");
            int number1 = Convert.ToInt16(ReadLine());

            Func<int, int> isPositive = number => number * number;

            Write(isPositive(number1) + "\n");
        }

    }
}