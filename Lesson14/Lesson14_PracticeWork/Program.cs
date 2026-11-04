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


      int[] numbers = { 1, 2, 8, -6, 4, 2, 7, 9, 16, 5 };

      int groupIndex = 0;

      var longestEvenGroup = numbers
    .Select(n => new { Number = n, Group = (n % 2 == 0) ? groupIndex : ++groupIndex })
    .Where(pair => pair.Number % 2 == 0)
    .GroupBy(pair => pair.Group)
    .OrderByDescending(group => group.Count())
    .FirstOrDefault();

if (longestEvenGroup != null)
{
    int[] resultSequence = longestEvenGroup.Select(pair => pair.Number).ToArray();
    int maxLength = resultSequence.Length;

    WriteLine($"\nДовжина найбільшої парної послідовності {maxLength}. Послідовність:");
    WriteLine(string.Join(" ", resultSequence));
}
else
{
    WriteLine("\nПарних чисел у масиві не знайдено.");
}


    }
}