namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       OutputEncoding = Encoding.UTF8;
       InputEncoding = Encoding.UTF8;


int[] numbers = { 1, 2, 8, 12, 15, 4, 2, 7, 9, 15, 5 };

WriteLine("Вхідний масив: " + string.Join(" ", numbers));

// ==========================================
// 1 ВАРІАНТ — Через LINQ GroupBy (Групування)
// ==========================================
int groupIndex = 0;

int maxLength1 = numbers
    .Select((n, i) => new
    {
        Number = n,
        // Якщо це перший елемент або він більший за попередній — залишаємо поточну групу.
        // Якщо число зменшилось або рівне — створюємо нову групу (++groupIndex)
        Group = (i == 0 || n > numbers[i - 1]) ? groupIndex : ++groupIndex
    })
    .GroupBy(pair => pair.Group)
    .Select(group => group.Count())
    .DefaultIfEmpty(0)
    .Max();

WriteLine($"\n[Варіант 1] Довжина найбільшої зростаючої послідовності: {maxLength1}");


// ==========================================
// 2 ВАРІАНТ — Через LINQ Aggregate
// ==========================================

// Накопичуємо стан у вигляді anonymous object:
// Current (поточна довжина), Max (максимальна довжина), Prev (попереднє число)
var result = numbers.Aggregate(
    new { Current = 0, Max = 0, Prev = int.MinValue },
    (state, n) =>
    {
        // Якщо це старт (Current == 0) або поточне число більше за попереднє
        bool isIncreasing = state.Current == 0 || n > state.Prev;

        // Збільшуємо лічильник або скидаємо його до 1 (починаємо нову послідовність)
        int nextCurrent = isIncreasing ? state.Current + 1 : 1;

        return new
        {
            Current = nextCurrent,
            Max = Math.Max(state.Max, nextCurrent),
            Prev = n
        };
    }
);

WriteLine($"[Варіант 2] Довжина найбільшої зростаючої послідовності: {result.Max}");

    }
}