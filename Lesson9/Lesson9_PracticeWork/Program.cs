using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
        public static void inputtoIntArr(int[] arr, int how_much )
        {
            for(int i =0; i < how_much; ++i)
            {
                Write("Write something message: ");
                arr[i] = Convert.ToInt16(ReadLine());
            }
        }
         static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            Write("how much el will be in array: ");
            int number = Convert.ToInt16(ReadLine());
            
             int[] arr = new int[number]; 

            inputtoIntArr(arr, number);
            
        Func<int, bool> isOdd = x => x % 2 != 0;

        int[] oddNumbers = arr.Where(isOdd).ToArray();

        WriteLine("\nНепарні числа з масиву:");
        foreach (int el in oddNumbers)
        {
            WriteLine(el);
        }
          
        }

    }
}