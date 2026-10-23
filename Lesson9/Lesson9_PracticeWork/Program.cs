using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
         static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            Write("Write a number for check is that even: ");
            int number = Convert.ToInt16(ReadLine());

            Predicate<int> IsEven = delegate(int x) 
            {
                return x % 2 == 0;
            };

            bool resultOld = IsEven(number); 

            Write($"{resultOld} \n");
        }

    }
}