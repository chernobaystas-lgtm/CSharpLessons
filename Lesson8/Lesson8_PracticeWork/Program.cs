
using System.Text.Unicode;
using static System.Console;


namespace Lesson8_PracticeWork
{
    class Program
    {
         static void Main(string[] args)
    {
        WriteLine("--- 1. Создание и инициализация Array1 ---");
        Array1 arr1 = new Array1();

        arr1.Add(15);
        arr1.Add(42);
        arr1.Add(7);
        arr1.Add(23);
        arr1.Add(42); 

        // Вывод через переопределенный метод ToString()
        WriteLine($"Создан массив arr1: {arr1}"); 
        WriteLine();

        WriteLine("--- 2. Тестирование интерфейсов вывода (IOutput) ---");
        arr1.Show1("Вывод через Show1(string):");
        WriteLine();
        arr1.Show2("Вывод через Show2(string):");
        WriteLine();

        try
        {
            arr1.Show1(""); // Должно выбросить ArgumentException
        }
        catch (ArgumentException)
        {
            WriteLine("Успешно перехвачено ArgumentException при передаче пустой строки!");
        }
        WriteLine();

        WriteLine("--- 3. Тестирование интерфейса IMath<int> ---");
        IMath<int> mathContract = arr1; 

        WriteLine($"Максимальный элемент: {mathContract.Max()}"); // 42
        WriteLine($"Минимальный элемент: {mathContract.Min()}");   // 7
        WriteLine($"Среднее арифметическое: {mathContract.Avg()}"); // 26.8

        // Поиск через int
        int searchIntTrue = 23;
        int searchIntFalse = 99;
        WriteLine($"Поиск числа {searchIntTrue}: {mathContract.Search(searchIntTrue)}"); // True
        WriteLine($"Поиск числа {searchIntFalse}: {mathContract.Search(searchIntFalse)}"); // False
        WriteLine();

        WriteLine("--- 4. Тестирование интерфейса IMathOBJ ---");
        IMathOBJ mathObjContract = arr1;

        object objInt = 42;
        object objString = "42";
        WriteLine($"Поиск объекта-числа ({objInt}): {mathObjContract.Search(objInt)}"); // True
        WriteLine($"Поиск объекта-строки (\"{objString}\"): {mathObjContract.Search(objString)}"); // False
        WriteLine();

        WriteLine("--- 5. Тестирование Equals и ToString() ---");
        Array1 arr2 = new Array1();
        arr2.Add(15);
        arr2.Add(42);
        arr2.Add(7);
        arr2.Add(23);
        arr2.Add(42);

        WriteLine($"Массив arr1: {arr1}");
        WriteLine($"Массив arr2: {arr2}");
        WriteLine($"Равны ли массивы по содержимому? {arr1.Equals(arr2)}"); // True

        WriteLine("\nИзменяем arr2 (удаляем элемент по индексу 2)...");
        arr2.Remove(2); // Удалит семерку
        WriteLine($"Новый массив arr2: {arr2}");
        WriteLine($"Равны ли они теперь? {arr1.Equals(arr2)}"); // False
        WriteLine();

        WriteLine("--- 6. Тестирование ручного ввода через Input() ---");
        WriteLine($"Текущий размер arr2 перед вводом: {arr2.ToString()}");
        WriteLine("Внимание: метод заполнит только существующие ячейки памяти.");
        
        // Метод Input() перезапишет элементы в arr2 в цикле на основе его текущего размера
        arr2.Input(); 
        
        WriteLine($"Результат ручного ввода в arr2: {arr2}");

        WriteLine("\nНажмите Enter для завершения программы...");
        ReadLine();
    }
    }
}

class Array1 : IOutput<string> , IOutput, IMath<int>, IMathOBJ, ISort
{
    private int[] arr;
    private int ArraySize;

    public Array1(){
        ArraySize = 0;
        arr = new int[0];
    }

public void Input()
{
    for (int i = 0; i < ArraySize; ++i)
    {
        while (true)
        {
            Write($"Введите целое число для элемента [{i}]: ");
            string? input = Console.ReadLine();

            // Проверяем, успешно ли строка преобразуется в число
            if (int.TryParse(input, out int result))
            {
                arr[i] = result;
                break; // Выходим из бесконечного цикла и переходим к следующему элементу
            }
            
            WriteLine("Ошибка! Вы ввели не число. Попробуйте еще раз.");
        }
    }
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

    public int Max()
    {
         if (ArraySize == 0)
    {
        throw new InvalidOperationException("Массив пуст. Невозможно найти максимум.");
    }

    int max = arr[0]; 

    for (int i = 1; i < ArraySize; i++)
    {
        if (arr[i] > max)
        {
            max = arr[i]; 
        }
    }

    return max;


    }

    public int Min()
    {
            if (ArraySize == 0)
    {
        throw new InvalidOperationException("Массив пуст. Невозможно найти минимум.");
    }

    int min = arr[0]; 

    for (int i = 1; i < ArraySize; i++)
    {
        if (arr[i] < min)
        {
            min = arr[i]; 
        }
    }

    return min;
    }

    public float Avg()
    {
    if (ArraySize == 0)
    {
        return 0f; 
    }

    float sum = 0; 
    int count = ArraySize;

    for (int i = 0; i < ArraySize; i++)
    {
        sum += arr[i]; 
    }

    return sum / count; 
    }

public bool Search(int valueToSearch)
{
    return System.Array.IndexOf(arr, valueToSearch) != -1;
}

public bool Search(object value)
{
    if (value is int elem)
    { 
        return (System.Array.IndexOf(arr, elem) == -1) ? false : true;
    }
    
    return false;
}

    // 1. Сортировка по возрастанию
    public void SortAsc()
    {
        if (ArraySize > 0)
        {
            System.Array.Sort(arr);
        }
    }

    // 2. Сортировка по убыванию
    public void SortDesc()
    {
        if (ArraySize > 0)
        {
            System.Array.Sort(arr);
            System.Array.Reverse(arr); 
        }
    }

    // 3. Сортировка в зависимости от параметра bool
    public void SortByParam(bool isAsc)
    {
        if (isAsc)
        {
            SortAsc();
        }
        else
        {
            SortDesc();
        }
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

internal interface IMath<T>
{
    int Max();
    int Min();

     float Avg();

     bool Search(int valueToSearch);
}

internal interface IMathOBJ
{
     bool Search(Object value);
}

public interface ISort
{
    void SortAsc();      
    void SortDesc();     
    void SortByParam(bool isAsc); 
}
