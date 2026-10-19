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
            new Rectangle(5, 10),
            new Ellipse(6, 4),
            new Circle(5),
            new Triangle(3, 4, 5)
        };

        foreach (var fig in figures)
        {
            Console.WriteLine(fig.ToString());
        }

        Console.ReadLine();
    }
}