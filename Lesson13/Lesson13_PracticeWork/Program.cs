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


      Student[] students = new Student[]
{
    new Student("Brown", 18, "Oxford"),
    new Student("Brody", 21, "Cambridge"),
    new Student("Smith", 22, "Oxford"),
    new Student("Broson", 25, "Harvard"),
    new Student("Wilson", 20, "Oxford"),
    new Student("Taylor", 19, "MIT"),
    new Student("Brooks", 23, "Oxford")
};

      foreach (var student in students)
      {
          WriteLine($"Student: {student.SecondName}, Age: {student.Age}, School: {student.School}");
      }


      foreach (var student in students.Where(s => s.SecondName.StartsWith("Bro")))
      {
          WriteLine($"Student: {student.SecondName}, Age: {student.Age}, School: {student.School}");
      }


      foreach (var student in students.Where(s => s.Age > 20))
      {
          WriteLine($"Student: {student.SecondName}, Age: {student.Age}, School: {student.School}");
      }

var filteredStudents = from student in students
                       where student.Age > 20 && student.Age < 23
                       orderby student.Age descending
                       select student;

WriteLine("\nСтуденты от 20 до 23 (Oxford/Сортировка):");
foreach (var student in filteredStudents)
{
    WriteLine($"Student: {student.SecondName}, Age: {student.Age}, School: {student.School}");
}

var oxfordStudents = from student in students
                     where student.School == "Oxford" && student.Age > 18
                     orderby student.Age descending
                     select student;

WriteLine("\nСтуденты из Oxford > 18 (сортировка по убыванию возраста):");
foreach (var student in oxfordStudents)
{
    WriteLine($"Student: {student.SecondName}, Age: {student.Age}, School: {student.School}");
}


    }
}

public record Student(string SecondName, int Age, string School);

