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

      foreach (int number in numbers)
      {
          WriteLine(number);
      }

      var evenNumbers = numbers.Where(n => n % 2 == 0);
      WriteLine("\nЧетные числа:");
      foreach (int even in evenNumbers)
      {
          WriteLine(even);
      }


      var oddNumbers = numbers.Where(n => n % 2 != 0);
      WriteLine("\nНечетные числа:");
      foreach (int odd in oddNumbers)
      {
          WriteLine(odd);
      }

      int start = 10;
      int end = 30;

var result = from n in numbers
             where n >= start && n <= end
             select n;

foreach (var num in result)
{
    WriteLine(num);
}


  var multiplesOfSeven = from n in numbers
                       where n % 7 == 0
                       orderby n
                       select n;

      WriteLine("\nКратные 7:");
      foreach (var num in multiplesOfSeven)
      {
          WriteLine(num);
      }


        var multiplesOfEight = from n in numbers
                       where n % 8 == 0
                       orderby n descending
                       select n;

      WriteLine("\nКратные 8:");
      foreach (var num in multiplesOfEight)
      {
          WriteLine(num);
      }

    }
}