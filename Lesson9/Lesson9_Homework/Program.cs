using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {

Action<int[]> showUniqueNegatives = (array) =>
{
    int[] seen = new int[array.Length];
    int seenCount = 0;

    foreach (int num in array)
    {
        if (num < 0)
        {
            bool alreadySeen = false;
            for (int i = 0; i < seenCount; i++)
            {
                if (seen[i] == num)
                {
                    alreadySeen = true;
                    break;
                }
            }

            if (!alreadySeen)
            {
                WriteLine(num);
                seen[seenCount] = num;
                seenCount++;
            }
        }
    }
};


static void Main(string[] args)
{
    int[] testArray = { 4, -5, 0, 12, -5, -3, 7, -3, -1, 8 };
    WriteLine("Source array: " + string.Join(", ", testArray));
    WriteLine("--- Unique negative numbers ---");

    // Сама лямбда
    Action<int[]> showUniqueNegatives = (array) =>
    {
        int[] seen = new int[array.Length];
        int seenCount = 0;

        foreach (int num in array)
        {
            if (num < 0)
            {
                bool alreadySeen = false;
                for (int i = 0; i < seenCount; i++)
                {
                    if (seen[i] == num)
                    {
                        alreadySeen = true;
                        break;
                    }
                }
                if (!alreadySeen)
                {
                    WriteLine(num);
                    seen[seenCount] = num;
                    seenCount++;
                }
            }
        }
    };

    // Вызов лямбды
    showUniqueNegatives(testArray);
}



    }
}
