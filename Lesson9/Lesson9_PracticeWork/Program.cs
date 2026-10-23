using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    public class IsThatNum
    {
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
             if (number < 2)
        {
            return false;
        }

        // достатньо шукати дільники до кореня з числа
        for (int i = 2; (long)i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
        }

        public static bool IsFubonaci(int number)
        {
            if (number < 0)
            {
            return false;
            }

            long previous = 0;
            long current = 1;

            while (previous < number)
            {
                long next = previous + current;
                previous = current;
                current = next;
            }

            return previous == number;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Write("Write a number to check");
            int number = Convert.ToInt32(ReadLine());

            Predicate<int> myPredicate = IsThatNum.IsOdd;
            bool isPrimeResult = myPredicate(number); 
            WriteLine($"[Через предикат] не просте: {isPrimeResult}");
            
             Predicate<int> myPredicate2 = IsThatNum.IsEven;
            bool isPrimeResult2 = myPredicate(number); 
            WriteLine($"[Через предикат] просте: {isPrimeResult}");

             Predicate<int> myPredicate3 = IsThatNum.IsPrime;
            bool isPrimeResult3 = myPredicate(number); 
            WriteLine($"[Через предикат] Просте: {isPrimeResult}");

             Predicate<int> myPredicate4 = IsThatNum.IsFubonaci;
            bool isPrimeResult4 = myPredicate(number); 
            WriteLine($"[Через предикат] фобаначи: {isPrimeResult}");

        }
    }
}