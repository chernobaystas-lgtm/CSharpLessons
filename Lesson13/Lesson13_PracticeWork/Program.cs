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

       int[] numbers = { 121, 75, 81, 5, 19, 43, -12, 90, 99, 101 };


int GetSumOfDigits(int number)
{
    string digits = Math.Abs(number).ToString();

    int sum = digits.Sum(ch => ch - '0');

    return sum;
}


var sortBySumAsc = from n in numbers
                   orderby GetSumOfDigits(n)
                   select n;


var sortBySumDesc = from n in numbers
                    orderby GetSumOfDigits(n) descending
                    select n;


WriteLine("\nСортировка по возрастанию суммы цифр:");
foreach (var n in sortBySumAsc)
{
    WriteLine($"{n} (сумма цифр: {GetSumOfDigits(n)})");
}

WriteLine("\nСортировка по убыванию суммы цифр:");
foreach (var n in sortBySumDesc)
{
    WriteLine($"{n} (сумма цифр: {GetSumOfDigits(n)})");
}


    }
}