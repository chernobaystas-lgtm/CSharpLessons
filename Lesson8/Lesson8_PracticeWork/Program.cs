
using System.Text.Unicode;
using static System.Console;


namespace Lesson8_PracticeWork
{
    class Program
    {
    static void Main(string[] args)
    {
        // 1. Создаем экземпляр нашего кастомного динамического массива
        Array myArr = new Array();

        WriteLine("=== 1. Проверка пустого массива ===");
        myArr.Show1(); // Выведет: Array is empty.
        WriteLine();

        WriteLine("=== 2. Добавление элементов (10, 20, 30) ===");
        myArr.Add(10);
        myArr.Add(20);
        myArr.Add(30);
        myArr.Show1(); // Выведет 10, 20, 30
        WriteLine();

        WriteLine("=== 3. Изменение элемента по индексу 1 ===");
        int oldVal = myArr.Change(1, 99);
        WriteLine($"Старое значение: {oldVal}"); // 20
        WriteLine("Текущий массив:");
        myArr.Show1(); // Выведет 10, 99, 30
        WriteLine();

        WriteLine("=== 4. Удаление элемента по индексу 0 ===");
        int removedVal = myArr.Remove(0);
        WriteLine($"Удалено значение: {removedVal}"); 
        WriteLine("Текущий массив:");
        myArr.Show1();
        WriteLine();

        WriteLine("=== 5. Тест методов Show1(string) и Show2(string) ===");
        myArr.Show1("--- Вывод через Show1 с инфой ---");
        WriteLine();
        myArr.Show2("--- Вывод через Show2 с инфой ---");
        WriteLine();

        WriteLine("=== 6. Тест Show2(object) со значением int ===");
        myArr.Show2(123); // Выведет элементы массива, так как сработал type pattern matching
        WriteLine();

        WriteLine("=== 7. Тест обработки исключений при пустой строке ===");
        try
        {
            myArr.Show1(""); 
        }
        catch (ArgumentException ex)
        {
            WriteLine($"Перехвачено исключение: {ex.GetType().Name} (Передана пустая строка info)");
        }

        ReadLine(); // Задержка экрана


        Array a1 = new Array();
        a1.Add(10);
        a1.Add(20);

        Array a2 = new Array();
        a2.Add(10);
        a2.Add(20);

        WriteLine(a1.ToString()); // Выведет красивое: [10, 20]
        WriteLine(a1.Equals(a2)); // Выведет: True (так как элементы одинаковые)

    }
    }
}

class Array : IOutput<string> , IOutput
{
    private int[] arr;
    private int ArraySize;

    public Array(){
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

    public void Show2(object? value)
    {
        if (value is int intValue)
        { 
           Show1();
        }
    }

    public void Show2(string? info)
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
        if (obj is Array other)
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
}

public interface IOutput<T>
{
    void Show1(T value);

    void Show1(string info);
}

public interface IOutput
{
    void Show2(Object value);

    void Show2(string info);
}