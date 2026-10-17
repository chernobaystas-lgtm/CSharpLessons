using System;
using System.Text;
using static System.Console;

namespace Lesson2_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 1: робота з одновимірним масивом A та двовимірним масивом B
            int[] a = new int[5];
            double[,] b = new double[3, 4];
            Random random = new Random();

            // A заповнює користувач
            for (int i = 0; i < a.Length; i++)
            {
                Write($"Введіть елемент A[{i}]: ");
                if (!int.TryParse(ReadLine(), out a[i]))
                {
                    WriteLine("Помилка: потрібно ввести ціле число!");
                    return;
                }
            }

            // B заповнюється випадковими дробовими числами
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    b[i, j] = Math.Round(random.NextDouble() * 20, 1);
                }
            }

            // Виведення масивів
            WriteLine();
            Write("Масив A: ");
            for (int i = 0; i < a.Length; i++)
            {
                Write(a[i] + " ");
            }
            WriteLine();

            WriteLine("Масив B:");
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    Write($"{b[i, j],7:F1}");
                }
                WriteLine();
            }

            // Обчислення
            double max = a[0];
            double min = a[0];
            double sum = 0;
            double product = 1;
            double evenSumA = 0;
            double oddColumnsSumB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > max) max = a[i];
                if (a[i] < min) min = a[i];
                sum += a[i];
                product *= a[i];

                if (a[i] % 2 == 0)
                {
                    evenSumA += a[i];
                }
            }

            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    if (b[i, j] > max) max = b[i, j];
                    if (b[i, j] < min) min = b[i, j];
                    sum += b[i, j];
                    product *= b[i, j];

                    // j нумерується з нуля, тому j = 0 і 2 це 1-й і 3-й стовпці
                    if (j % 2 == 0)
                    {
                        oddColumnsSumB += b[i, j];
                    }
                }
            }

            WriteLine();
            WriteLine($"Максимальний елемент: {max}");
            WriteLine($"Мінімальний елемент: {min}");
            WriteLine($"Загальна сума всіх елементів: {sum:F2}");
            WriteLine($"Загальний добуток всіх елементів: {product:F2}");
            WriteLine($"Сума парних елементів масиву A: {evenSumA}");
            WriteLine($"Сума непарних стовпців масиву B: {oddColumnsSumB:F2}");
        }
    }
}