using System;


class Program
{
  static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Course[] courses = new Course[]
        {
            new Course("Вступ до програмування", 40),
            new OnlineCourse("C# Базовий", 60, "IT Step"),
            new OnlineCourse("Проектування БД", 45, "Coursera")
        };

        Console.WriteLine("=== УСІ КУРСИ, ЩО Є У НАЯВНОСТІ ===");
        foreach (Course course in courses)
        {
            Console.WriteLine(course.ToString());
        }

        Console.WriteLine("\n=== ЛИШЕ КУРСИ ONLINECOURSE ===");
        foreach (Course course in courses)
        {
            if (course is OnlineCourse)
            {
                Console.WriteLine(course.ToString());
            }
        }

        Console.ReadLine();
    }
}