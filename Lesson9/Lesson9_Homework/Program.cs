using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {

        Func<int[], int> countPositives = (array) =>
{
    int count = 0;
    foreach (int num in array)
    {
        if (num > 0)
        {
            count++;
        }
    }
    return count;
};

static void Main(string[] args)
{



    int[] testArray = { 4, -5, 0, 12, -5, -3, 7, -3, -1, 8 };
    WriteLine("Source array: " + string.Join(", ", testArray));

    // Наша лямбда
    Func<int[], int> countPositives = (array) =>
    {
        int count = 0;
        foreach (int num in array)
        {
            if (num > 0) count++;
        }
        return count;
    };

    // Вызов и вывод результата
    int result = countPositives(testArray);
    WriteLine($"Number of positive elements: {result}");
}



    }
}
