namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;

class Program
{
    static T MinElem<T>(T[] arr) where T : IComparable<T>
        {
            T min = arr[0];
            foreach (T item in arr)
            {
                if (item.CompareTo(min) < 0)
                    min = item;
            }
            return min;
        }

      static T MaxElem<T>(T[] arr) where T : IComparable<T>
        {
            T max = arr[0];
            foreach (T item in arr)
            {
                if (item.CompareTo(max) > 0)
                    max = item;
            }
            return max;
        }

    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        try{

            WriteLine($"Максимальний елемент: {MinElem<int>(numbers)}");

            double[] arrDouble = new double[] { 45.62, 77.354, 18.48, 11.3 };

            WriteLine($"Максимальний елемент: {MinElem(arrDouble)}");

        }
        catch(ArgumentNullException ex)
        {
            WriteLine(ex.Message);
        }

    }
}

