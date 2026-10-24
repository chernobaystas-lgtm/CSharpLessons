using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {

static void Main(string[] args)
{
    WriteLine("--- Task 8: Filtering Array using Extension Method ---");
    
    int[] testArray = { 4, -5, 0, 12, -5, -3, 7, -3, -1, 8 };
    WriteLine("Source array: " + string.Join(", ", testArray));

    // Создаем предикат (условие) для проверки четности
    Predicate<int> isEvenPredicate = (num) => num % 2 == 0;
    
    // Вызываем наш метод расширения
    int[] filteredArray = testArray.FilterByPredicate(isEvenPredicate);
    WriteLine("Filtered array (even numbers only): " + string.Join(", ", filteredArray));
}

    }
}



public static class Extensions
{
    public static int[] FilterByPredicate(this int[] array, Predicate<int> condition)
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
}
