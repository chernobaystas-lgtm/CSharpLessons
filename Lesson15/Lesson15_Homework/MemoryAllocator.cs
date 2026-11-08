using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;
using static System.GC;


class MemoryAllocator : IDisposable
{
    private string[]? _largeArray;
    private bool _disposed = false;

    // 1. Створює масив об'єктів розміром 1 млн елементів
    public MemoryAllocator()
    {
        _largeArray = new string[1_000_000];
    }

    // 2. Симулює активне використання пам'яті
    public void AllocateMemory()
    {
        if (_disposed || _largeArray == null) return;

        for (int i = 0; i < _largeArray.Length; i++)
        {
            _largeArray[i] = "МусорныйРядок_" + i;
        }
    }

    // Метод для визначення покоління створеного масиву
    public int GetArrayGeneration()
    {
        if (_largeArray == null) return -1;
        return GetGeneration(_largeArray); // Из System.GC
    }

    // Реалізація інтерфейсу IDisposable для явного звільнення ресурсів
    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            if (_largeArray != null)
            {
                Array.Clear(_largeArray, 0, _largeArray.Length);
                _largeArray = null;
            }
            WriteLine("[Dispose]: Пам'ять об'єкта MemoryAllocator успішно очищена вручну.");
        }
        else
        {
            WriteLine("[Деструктор]: Пам'ять очищено автоматично збирачем сміття.");
        }

        _disposed = true;
    }

    ~MemoryAllocator()
    {
        Dispose(false);
    }
}