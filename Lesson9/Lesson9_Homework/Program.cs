using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
static void Main(string[] args)
{
    int[] numbers = { 1, 2, 3, 4, 5, 8, 11, 13, 14, 21, 25, 34 };
    WriteLine("Original array: " + string.Join(", ", numbers));

    // 1. Чётные
    int[] evenNumbers = ArrayFilter.Filter(numbers, ArrayFilter.IsEven);
    WriteLine("Even numbers: " + string.Join(", ", evenNumbers));

    // 2. Нечётные
    int[] oddNumbers = ArrayFilter.Filter(numbers, ArrayFilter.IsOdd);
    WriteLine("Odd numbers: " + string.Join(", ", oddNumbers));

    // 3. Простые
    int[] primeNumbers = ArrayFilter.Filter(numbers, ArrayFilter.IsPrime);
    WriteLine("Prime numbers: " + string.Join(", ", primeNumbers));

    // 4. Фибоначчи
    int[] fibNumbers = ArrayFilter.Filter(numbers, ArrayFilter.IsFibonacci);
    WriteLine("Fibonacci numbers: " + string.Join(", ", fibNumbers));
}



    }
}

public static class ArrayFilter
{
  
    public static int[] Filter(int[] array, Predicate<int> condition)
    {
        int count = 0;
        foreach (int num in array)
        {
            if (condition(num))
            {
                count++;
            }
        }

        int[] result = new int[count];
        int index = 0;
        foreach (int num in array)
        {
            if (condition(num))
            {
                result[index] = num;
                index++;
            }
        }

        return result;
    }

    public static bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }
    public static bool IsPrime(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    public static bool IsFibonacci(int number)
    {
        if (number < 0) return false;
        return IsPerfectSquare(5 * number * number + 4) || IsPerfectSquare(5 * number * number - 4);
    }

    private static bool IsPerfectSquare(int x)
    {
        int s = (int)Math.Sqrt(x);
        return s * s == x;
    }
}