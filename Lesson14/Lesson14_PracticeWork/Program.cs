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


       List<int> numbers = new() {19, 3, -2,0,12,12,1000,-3,  1, 29,-12,381,2,-1,23,23,45,12,56,54,-31, 1000, -902, 1400};

      var howManyNumbers = numbers.GroupBy(x =>x).Select(g => new { Value = g.Key, Count = g.Count() }).Where(x => x.Count > 1).OrderByDescending(x => x.Count);
      howManyNumbers.ToList().ForEach(n => WriteLine($"Value: {n.Value} - Count: {n.Count}"));

      Write("\n");

      var howManyNumbers2 = numbers.GroupBy(x =>x).Select(g => new { Value = g.Key, Count = g.Count() }).Where(x => x.Count > 1 && x.Value % 2 == 0).OrderByDescending(x => x.Count);
      howManyNumbers2.ToList().ForEach(n => WriteLine($"Value: {n.Value} - Count: {n.Count}"));

      int evenNumbers = numbers.Count(x => x % 2 == 0);
      WriteLine($"Even numbers: {evenNumbers}");

      int oddNumbers = numbers.Count(x => x % 2 != 0);
      WriteLine($"Odd numbers: {oddNumbers}");
    }
}