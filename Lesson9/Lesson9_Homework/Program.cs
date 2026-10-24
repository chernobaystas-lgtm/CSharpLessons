using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {
static void Main(string[] args)
{
    WriteLine("--- Testing Lambda for Counting Numbers Multiple of 7 ---");

    Func<int[], int> countMultiplesOfSeven = (array) =>
    {
        int count = 0;
        foreach (int num in array)
        {
            if (num % 7 == 0)
            {
                count++;
            }
        }
        return count;
    };

    int[] testArray = { 1, 7, 10, 14, 21, 25, 30, 35, 44, 0, -7 };
    WriteLine("Source array: " + string.Join(", ", testArray));

    // Викликаємо лямбду і виводимо результат
    int result = countMultiplesOfSeven(testArray);
    WriteLine($"Number of elements multiple of 7: {result}");
}




    }
}
