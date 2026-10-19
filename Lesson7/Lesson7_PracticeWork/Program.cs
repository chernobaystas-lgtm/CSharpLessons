using System;
using System.Text;
using static System.Console;

namespace Lesson7_PracticeWork;

class Program
{
        static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Figure[] figures = new Figure[]
        {
            new Rectangle(5, 4),           
            new Circle(3),                
            new RightTriangle(6, 4),        
            new Trapezoid(3, 7, 5)         
        };

        Console.WriteLine("=== Результаты подсчета площадей ===");
        
        // Полиморфизм в действии: вызываем GetArea для каждого объекта
        foreach (var fig in figures)
        {
            fig.DisplayInfo();
            Console.WriteLine(new string('-', 40));
        }

        Console.ReadLine();
    }
}