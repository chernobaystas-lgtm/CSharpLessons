using System.Text.Unicode;
using Microsoft.VisualBasic;
using static System.Console;


namespace Lesson8_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {

            Write("Enter count of elements: ");
            int count = Convert.ToInt32(ReadLine());

            int[] myArr = new int[count];

            Array1 dynamicArray = new Array1();
            dynamicArray.Input(count, myArr);

            Write("Enter number for compare it with element in arrat: ");
            int number = Convert.ToInt32(ReadLine());

            dynamicArray.Less(number, myArr);

            dynamicArray.Greater(number, myArr);

            WriteLine("\n");

        }
    }
}

public interface ICalc<T>
{
    int Less(T valueToCompare, int[] arr);

    int Greater(T valueToCompare, int[] arr);

}

class Array1 : ICalc<int>
{
    // My own class
    private int[] arr;
    private int ArraySize;

    public Array1(){
        ArraySize = 0;
        arr = new int[0];
    }

    public void Add(int value)
    {
        int[] newArr = new int[ArraySize + 1];

        for (int i = 0; i < ArraySize; i++)
        {
            newArr[i] = arr[i];
        }

        newArr[ArraySize] = value;
        arr = newArr;
        ArraySize++;
    }

    public int Remove(int index)
    {
        if (index < 0 || index >= ArraySize)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        int removedValue = arr[index];
        int[] newArr = new int[ArraySize - 1];

        for (int i = 0, j = 0; i < ArraySize; i++)
        {
            if (i != index)
            {
                newArr[j++] = arr[i];
            }
        }

        arr = newArr;
        ArraySize--;
        return removedValue;
    }

    public int Change(int index, int newValue)
    {
        if (index < 0 || index >= ArraySize)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        int oldValue = arr[index];
        arr[index] = newValue;
        return oldValue;
    }

    public void Show1()
    { 
        if (ArraySize == 0)
        {
            WriteLine("Array is empty.");
            return;
        }

        for(int i = 0; i < ArraySize; i++)
        {
            WriteLine(arr[i]);
        }
    }

    public void Show1(string? info)
    {
        if (string.IsNullOrEmpty(info))
        { 
           WriteLine("No info");
           throw new ArgumentException();
        }
        WriteLine(info);
        Show1();
    }
    
    public void Input(int? how_much, int[] arr)
    {   Write($"Write numbers for array: ");

        for(int i = 0; i < how_much; i++)
        {
            Write($"{i + 1}: ");
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            arr[i] = value;
        }
        else
        {
            Console.WriteLine("Invalid input, saved as 0.");
            arr[i] = 0; 
        }
        }
    }

 public override string ToString()
    {
        if (ArraySize == 0) return "[]";
        return "[" + string.Join(", ", arr) + "]";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Array1 other)
        {
            if (this.ArraySize != other.ArraySize)
            {
                return false;
            }

            // Поэлементная проверка
            for (int i = 0; i < ArraySize; i++)
            {
                if (this.arr[i] != other.arr[i])
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        
        hash.Add(ArraySize);
        for (int i = 0; i < ArraySize; i++)
        {
            hash.Add(arr[i]);
        }
        
        return hash.ToHashCode();
    }

    public int Less(int valueToCompare, int[] arr)
    {
        int count = 0;
        WriteLine($"\n--- Checking numbers less than {valueToCompare} ---");
        
        foreach (int el in arr)
        {
            if (el < valueToCompare)
            {
                count++;
                WriteLine($"{el} is less than {valueToCompare}");
            }
        }
        WriteLine($"Result: {count} numbers in array are less than {valueToCompare}\n");
        return count;
    }

    public int Greater(int valueToCompare, int[] arr)
    {
        int count = 0;
        WriteLine($"--- Checking numbers greater than {valueToCompare} ---");
        
        foreach (int el in arr)
        {
            if (el > valueToCompare)
            {
                count++;
                WriteLine($"{el} is bigger than {valueToCompare}");
            }
        }
        WriteLine($"Result: {count} numbers in array are bigger than {valueToCompare}\n");
        return count;
    }

}

