using System;
using System.Text;
using static System.Console;

namespace Lesson2_PracticeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = Encoding.UTF8;
            InputEncoding = Encoding.UTF8;

            // Завдання 4: спільні елементи двох масивів без повторень у третьому масиві
            Write("Введіть розмір першого масиву M: ");
            if (!int.TryParse(ReadLine(), out int m) || m <= 0)
            {
                WriteLine("Помилка: розмір має бути цілим числом більше нуля!");
                return;
            }

            Write("Введіть розмір другого масиву N: ");
            if (!int.TryParse(ReadLine(), out int n) || n <= 0)
            {
                WriteLine("Помилка: розмір має бути цілим числом більше нуля!");
                return;
            }

            Random random = new Random();
            int[] first = new int[m];
            int[] second = new int[n];

            Write("Перший масив: ");
            for (int i = 0; i < first.Length; i++)
            {
                first[i] = random.Next(1, 11);
                Write(first[i] + " ");
            }
            WriteLine();

            Write("Другий масив: ");
            for (int i = 0; i < second.Length; i++)
            {
                second[i] = random.Next(1, 11);
                Write(second[i] + " ");
            }
            WriteLine();

            int[] result = new int[Math.Min(m, n)];
            int resultCount = 0;

            for (int i = 0; i < first.Length; i++)
            {
                bool inSecond = false;
                for (int j = 0; j < second.Length; j++)
                {
                    if (first[i] == second[j])
                    {
                        inSecond = true;
                        break;
                    }
                }

                bool alreadyAdded = false;
                for (int k = 0; k < resultCount; k++)
                {
                    if (result[k] == first[i])
                    {
                        alreadyAdded = true;
                        break;
                    }
                }

                if (inSecond && !alreadyAdded)
                {
                    result[resultCount] = first[i];
                    resultCount++;
                }
            }

            Write("Спільні елементи: ");
            if (resultCount == 0)
            {
                WriteLine("немає");
            }
            else
            {
                for (int i = 0; i < resultCount; i++)
                {
                    Write(result[i] + " ");
                }
                WriteLine();
            }
        }
    }
}