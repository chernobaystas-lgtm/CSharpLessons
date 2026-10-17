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

            // Завдання 4: операції над матрицями
            WriteLine("1 - множення матриці на число");
            WriteLine("2 - додавання матриць");
            WriteLine("3 - добуток матриць");
            Write("Ваш вибір: ");
            string choice = ReadLine()!;

            Random random = new Random();

            if (choice == "1")
            {
                int rows = ReadSize("Кількість рядків: ");
                int cols = ReadSize("Кількість стовпців: ");
                if (rows == 0 || cols == 0)
                {
                    WriteLine("Помилка: розмір має бути цілим числом більше нуля!");
                    return;
                }

                Write("Введіть число: ");
                if (!int.TryParse(ReadLine(), out int number))
                {
                    WriteLine("Помилка: потрібно ввести ціле число!");
                    return;
                }

                int[,] a = CreateMatrix(rows, cols, random);
                int[,] result = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = a[i, j] * number;
                    }
                }

                PrintMatrix("Матриця A:", a);
                PrintMatrix($"Результат A * {number}:", result);
            }
            else if (choice == "2")
            {
                int rows = ReadSize("Кількість рядків: ");
                int cols = ReadSize("Кількість стовпців: ");
                if (rows == 0 || cols == 0)
                {
                    WriteLine("Помилка: розмір має бути цілим числом більше нуля!");
                    return;
                }

                int[,] a = CreateMatrix(rows, cols, random);
                int[,] b = CreateMatrix(rows, cols, random);
                int[,] result = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] = a[i, j] + b[i, j];
                    }
                }

                PrintMatrix("Матриця A:", a);
                PrintMatrix("Матриця B:", b);
                PrintMatrix("Результат A + B:", result);
            }
            else if (choice == "3")
            {
                int rowsA = ReadSize("Кількість рядків матриці A: ");
                int colsA = ReadSize("Кількість стовпців матриці A (= рядків матриці B): ");
                int colsB = ReadSize("Кількість стовпців матриці B: ");
                if (rowsA == 0 || colsA == 0 || colsB == 0)
                {
                    WriteLine("Помилка: розмір має бути цілим числом більше нуля!");
                    return;
                }

                int[,] a = CreateMatrix(rowsA, colsA, random);
                int[,] b = CreateMatrix(colsA, colsB, random);
                int[,] result = new int[rowsA, colsB];

                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < colsB; j++)
                    {
                        for (int k = 0; k < colsA; k++)
                        {
                            result[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }

                PrintMatrix("Матриця A:", a);
                PrintMatrix("Матриця B:", b);
                PrintMatrix("Результат A * B:", result);
            }
            else
            {
                WriteLine("Помилка: потрібно ввести 1, 2 або 3!");
            }
        }

        // Читає розмір матриці. Якщо ввели не додатне ціле число, повертає 0
        static int ReadSize(string message)
        {
            Write(message);
            if (int.TryParse(ReadLine(), out int size) && size > 0)
            {
                return size;
            }
            return 0;
        }

        // Створює матрицю і заповнює її випадковими числами від 1 до 9
        static int[,] CreateMatrix(int rows, int cols, Random random)
        {
            int[,] matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }

            return matrix;
        }

        static void PrintMatrix(string title, int[,] matrix)
        {
            WriteLine();
            WriteLine(title);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Write($"{matrix[i, j],6}");
                }
                WriteLine();
            }
        }
    }
}