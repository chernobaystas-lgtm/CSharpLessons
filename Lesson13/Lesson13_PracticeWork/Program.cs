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

      Random rand = new Random();
List<int> numbers = new List<int>();

for (int i = 0; i < 50; i++)
{
    numbers.Add(rand.Next(-100, 101));
}
    numbers.ForEach(n => WriteLine(n));

      var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
      WriteLine("\nЧетные числа:");
evenNumbers.ForEach(n => WriteLine(n));


      var oddNumbers = numbers.Where(n => n % 2 != 0).ToList();
      WriteLine("\nНечетные числа:");
      oddNumbers.ForEach(n => WriteLine(n));

      int start = 10;
      int end = 30;

var result = (from n in numbers
             where n >= start && n <= end
             select n).ToList();

result.ForEach(n => WriteLine(n));


  var multiplesOfSeven = (from n in numbers
                       where n % 7 == 0
                       orderby n
                       select n).ToList();

      WriteLine("\nКратные 7:");
      multiplesOfSeven.ForEach(n => WriteLine(n));


        var multiplesOfEight = (from n in numbers
                       where n % 8 == 0
                       orderby n descending
                       select n).ToList();

      WriteLine("\nКратные 8:");
      multiplesOfEight.ForEach(n => WriteLine(n));

    }
}