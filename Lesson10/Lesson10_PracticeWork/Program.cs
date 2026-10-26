namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using static System.Convert;
using static System.Array;
using static System.Exception;

class Program
{
    static void Main(string[] args)
    { 
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int[] evenNumbers = numbers.Filter(x => x % 2 == 0);
            WriteLine("Чётные числа: " + string.Join(", ", evenNumbers));

            int[] oddNumbers = numbers.Filter(x => x % 2 != 0);
            WriteLine("Нечётные числа: " + string.Join(", ", oddNumbers));
    }


  public static int[] Filter(this int[] array, Predicate<int> predicate)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            int count = 0;
            foreach (int item in array)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            int[] resultArray = new int[count];

            int index = 0;
            foreach (int item in array)
            {
                if (predicate(item))
                {
                    resultArray[index] = item;
                    index++; 
                }
            }

            return resultArray;
        }
}



class Array1 
{
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

      public void InputElements(int how_much)
    {
        WriteLine($"Write {how_much} numbers for array:");
        for (int i = 0; i < how_much; i++)
        {
            Write($"Element {i + 1}: ");
            if (int.TryParse(ReadLine(), out int value))
            {
                Add(value); 
            }
            else
            {
                WriteLine("Invalid input, saved as 0.");
                Add(0);
            }
        }
    }


     public void ShowEven()
    {
        WriteLine("\n--- Even numbers in array ---");
        bool hasEven = false;
        for (int i = 0; i < ArraySize; i++)
        {
            if (arr[i] % 2 == 0)
            {
                WriteLine(arr[i]);
                hasEven = true;
            }
        }
        if (!hasEven) WriteLine("No even numbers found.");
    }

    public void ShowOdd()
    {
        WriteLine("\n--- Odd numbers in array ---");
        bool hasOdd = false;
        for (int i = 0; i < ArraySize; i++)
        {
            if (arr[i] % 2 != 0)
            {
                WriteLine(arr[i]);
                hasOdd = true;
            }
        }
        if (!hasOdd) WriteLine("No odd numbers found.");
    }
}

