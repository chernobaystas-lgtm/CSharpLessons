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

int[] array1 = { 1, 2, 2, 3, 4, 5, 5 };
int[] array2 = { 3, 4, 6, 7 };

var difference = from num1 in array1
                 where !(from num2 in array2 select num2).Contains(num1)
                 select num1;
 WriteLine($"Різниця: {string.Join(", ", difference)}");

var intersection = from num1 in array1
                   from num2 in array2
                   where num1 == num2
                   select num1;
 WriteLine($"Перетин: {string.Join(", ", intersection)}");

var union = from num in (from n1 in array1 select n1).Concat(from n2 in array2 select n2)
            group num by num into g
            select g.Key;
 WriteLine($"Об'єднання: {string.Join(", ", union)}");

var distinctArray1 = from num in array1
                     group num by num into g
                     select g.Key;
 WriteLine($"Перший масив без повторень: {string.Join(", ", distinctArray1)}");


    }
}