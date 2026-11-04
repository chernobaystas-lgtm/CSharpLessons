namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       OutputEncoding = Encoding.UTF8;
       InputEncoding = Encoding.UTF8;


       List<int> numbers = new() {19, 3, -2,0,  1, 29,-12,381,2,-1,23,23,45,12,56,54,-31, 1000, -902, 1400};

        long totalProduct = numbers.Aggregate((acc, n) => acc * n);
        WriteLine("Total Product: " + totalProduct + "\n");


        WriteLine("Count: " + numbers.Count() + "\n");

        int count = numbers.Count(x => x % 9 == 0);
        WriteLine("Count of multiples of 9: " + count + "\n");

        int countandbigger = numbers.Count(x => x % 7 == 0 && x > 945);
        WriteLine("Count of multiples of 7 and greater than 945: " + countandbigger + "\n");

        WriteLine("Sum: " + numbers.Sum() + "\n");

        WriteLine("Sum of even numbers: " + numbers.Where(x => x % 2 == 0).Sum() + "\n");

        WriteLine("Minimum: " + numbers.Min() + "\n");
        WriteLine("Maximum: " + numbers.Max() + "\n");
        WriteLine("Average: " + numbers.Average() + "\n");
    }
}