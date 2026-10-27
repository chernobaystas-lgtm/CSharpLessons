using System;
using System.Text;
using static System.Console;

namespace Lesson10_Homework;

// Запис «Оцінки студента»: ім'я та оцінки з набору предметів
record StudentGrades(string Name, int[] Grades)
{
    public double Average
    {
        get
        {
            int sum = 0;

            for (int i = 0; i < Grades.Length; i++)
            {
                sum += Grades[i];
            }

            return (double)sum / Grades.Length;
        }
    }
}

class Program
{
    // Максимальна оцінка (12-бальна шкала). Якщо в вас інша, змініть тут
    const int MaxGrade = 12;

    static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;
        InputEncoding = Encoding.UTF8;

        int studentCount = ReadInt("Скільки студентів: ", 1, 100);
        int subjectCount = ReadInt("Скільки предметів: ", 1, 20);

        StudentGrades[] students = new StudentGrades[studentCount];

        for (int i = 0; i < students.Length; i++)
        {
            WriteLine();
            Write($"Ім'я студента {i + 1}: ");
            string name = ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                name = $"Студент {i + 1}";
            }

            int[] grades = new int[subjectCount];

            for (int j = 0; j < grades.Length; j++)
            {
                grades[j] = ReadInt($"  Оцінка з предмета {j + 1} (1-{MaxGrade}): ", 1, MaxGrade);
            }

            students[i] = new StudentGrades(name.Trim(), grades);
        }

        // таблиця всіх студентів
        WriteLine();
        WriteLine($"{"Студент",-20}{"Середня",8}");

        for (int i = 0; i < students.Length; i++)
        {
            WriteLine($"{students[i].Name,-20}{students[i].Average,8:F2}");
        }

        // шукаємо студента з максимальною середньою оцінкою
        StudentGrades best = students[0];

        for (int i = 1; i < students.Length; i++)
        {
            if (students[i].Average > best.Average)
            {
                best = students[i];
            }
        }

        WriteLine();
        WriteLine($"Найвища середня оцінка у студента {best.Name}: {best.Average:F2}");

        Write("Оцінки: ");

        for (int i = 0; i < best.Grades.Length; i++)
        {
            Write(best.Grades[i] + " ");
        }

        WriteLine();
    }

    // Питає ціле число в діапазоні min-max, поки не введуть коректне
    static int ReadInt(string text, int min, int max)
    {
        while (true)
        {
            Write(text);

            if (int.TryParse(ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            WriteLine($"Треба ввести ціле число від {min} до {max}, ще раз.");
        }
    }
}