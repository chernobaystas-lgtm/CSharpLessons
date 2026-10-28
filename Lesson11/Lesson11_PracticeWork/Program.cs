namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;
using System.Numerics;

class Program
{
    static T SumElem<T>(T[] arr) where T : IComparable<T>, INumber<T>
      {
        T sum = arr[0];
        for (int i = 1; i < arr.Length; ++i)
        {
            sum += arr[i];
        }

        return sum;
      }

    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        try{

            WriteLine($"Сумма елементов: {SumElem<int>(numbers)}");

            double[] arrDouble = new double[] { 45.62, 77.354, 18.48, 11.3 };

            WriteLine($"Сумма елементов: {SumElem(arrDouble)}");

        }
        catch (ArgumentNullException ex) // Это стандартное исключение C# для null
        {
          WriteLine("Ошибка: переданный массив не существует или равен null.");
          WriteLine(ex.Message);
        }
    }
}

