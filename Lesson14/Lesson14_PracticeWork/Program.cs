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

       var top3 = numbers.OrderByDescending(x => x).Take(3).ToArray();
       top3.ToList().ForEach(WriteLine);
       Write("\n");
       var low3 = numbers.OrderBy(x => x).Take(3).ToArray();
       low3.ToList().ForEach(WriteLine);

    }
}